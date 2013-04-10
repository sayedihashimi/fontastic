function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value
    Split-Path $Invocation.MyCommand.Path
}

$downloadFile = $false
$fontsUrl = 'http://www.edgefonts.com/#list-of-available-fonts'

$scriptDir = Get-ScriptDirectory
$htmlResultPath = Join-Path $scriptDir 'BuildOutput\fonts.html'
$xmlResultPath = Join-Path $scriptDir 'BuildOutput\fonts.xml'


if($downloadFile -and (Test-Path $htmlResultPath)){
#    Remove-Item $htmlResultPath
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


"Creating XML file" | Write-Host -ForegroundColor Cyan
$lines = @()
$lines += '<fonts>'
for($i = $startTableLine; $i -le $endTableLine; $i++){
    $lines += $htmlContent[$i]
}
$lines += '</fonts>'

Set-Content -Path $xmlResultPath -Value $lines


$assemblyPath = Join-Path $scriptDir 'BuildOutput\Edgefonts.dll'
[System.Reflection.Assembly]::LoadFile($assemblyPath)

$parser = New-Object EdgeFonts.FontInfoParser
$fontInfoList = $parser.GenerateFromHtmlFile($xmlResultPath)

"Number of fonts found: [{0}]" -f $fontInfoList.Length

foreach($font in $fontInfoList){
    "{0} | {1} | {2}" -f $font.FamilyDisplayName, $font.Family,$font.AvailableFontVariations.length | Write-Host -ForegroundColor DarkCyan
}

"Script directory {0}" -f $scriptDir | Write-Host -ForegroundColor DarkCyan
"Assembly path {0}" -f $assemblyPath | Write-Host -ForegroundColor DarkCyan