function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value
    Split-Path $Invocation.MyCommand.Path
}

function Write-InfoMessage
{
    param([string]$message)
    if($message){
        $message | Write-Host -ForegroundColor Cyan
    }
}

$downloadFile = $false
$fontsUrl = 'http://www.edgefonts.com/#list-of-available-fonts'

$scriptDir = Get-ScriptDirectory
$edgeFontsAssemblyPath = Join-Path $scriptDir 'BuildOutput\Edgefonts.dll'
$jsonNetAssemblyPath = Join-Path $scriptDir 'Lib\Newtonsoft.Json.dll'
$htmlResultPath = Join-Path $scriptDir 'BuildOutput\fonts.html'
$xmlResultPath = Join-Path $scriptDir 'BuildOutput\fonts.xml'
$jsonResultPath = Join-Path $scriptDir 'BuildOutput\fonts.js'


if($downloadFile -and (Test-Path $htmlResultPath)){
    Remove-Item $htmlResultPath
}

$webClient = New-Object System.Net.WebClient
if($downloadFile){

    $webClient.DownloadFile($fontsUrl,$htmlResultPath)
}

if(!(Test-Path $htmlResultPath)){
    "The html file was not downloaded to [{0}]" -f $htmlResultPath | Write-Error
    exit 1
}

# we cannot send the file as-is to an XML parser. We have to extract out just the tables
$htmlContent = Get-Content $htmlResultPath
$startTableLine = -1
$endTableLine = -1

$foundStart = $false
for($i = 0; $i -lt $htmlContent.Length; $i++){
    $line = $htmlContent[$i]
    if($line -eq $null){
        $line = ''
    }

    $line = $line.Trim()

    if($startTableLine -le 0){
        if($line.StartsWith('<table>',[System.StringComparison]::OrdinalIgnoreCase)){
            $startTableLine = $i
        }
    }

    if($line.StartsWith('</table>',[System.StringComparison]::OrdinalIgnoreCase)){
        $endTableLine = $i
    }
}

if( ($startTableLine -le 0) -or 
    ($endTableLine -le 0) ) {
    "The font tables were not found in the file [{0}]" -f $htmlResultPath | Write-Error
    exit 1
    }


"Creating XML file" | Write-InfoMessage
$lines = @()
$lines += '<fonts>'
for($i = $startTableLine; $i -le $endTableLine; $i++){
    $lines += $htmlContent[$i]
}
$lines += '</fonts>'

Set-Content -Path $xmlResultPath -Value $lines



[System.Reflection.Assembly]::LoadFile($edgeFontsAssemblyPath)

$parser = New-Object EdgeFonts.FontInfoParser
$fontInfoList = $parser.GenerateFromHtmlFile($xmlResultPath)

"Number of fonts found: [{0}]" -f $fontInfoList.Length

# TODO: remove this later
foreach($font in $fontInfoList){
    "{0} | {1} | {2}" -f $font.FamilyDisplayName, $font.Family,$font.AvailableFontVariations.length | Write-Host -ForegroundColor DarkCyan
}

"Serializing to json" | Write-InfoMessage
# searlize it to Json and write the file out
[System.Reflection.Assembly]::LoadFile($jsonNetAssemblyPath)
$fontsJson = [Newtonsoft.Json.JsonConvert]::SerializeObject($fontInfoList)
if($fontsJson -eq $null -or $fontsJson.Length -le 0){
    "The font did not serialize to Json correctly. It is null or empty" | Write-Error
    exit 1
}

"Writing json out to file [{0}]" -f $jsonResultPath  | Write-InfoMessage
Set-Content -Value $fontsJson -Path $jsonResultPath

"Script directory {0}" -f $scriptDir  | Write-InfoMessage
"Assembly path {0}" -f $edgeFontsAssemblyPath  | Write-InfoMessage