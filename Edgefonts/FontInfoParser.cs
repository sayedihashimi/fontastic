namespace Edgefonts {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Edgefonts.Extensions;
    using System.IO;

    public interface IFontInfoParser {
        IFontInfo BuildFromHtmlTable(string htmlTable);
        IFontInfo BuildFromHtmlTable(System.Xml.Linq.XElement element);
        System.Collections.Generic.IList<IFontInfo> GenerateFromHtmlFile(string filePath);
        System.Collections.Generic.IList<IFontInfo> GenerateFromHtmlTables(string html);
        System.Collections.Generic.IList<IFontInfo> ReadFontsFromJson(string json);
        System.Collections.Generic.IList<IFontInfo> ReadFontsFromJsonFile(string filePath);
    }

    public class FontInfoParser : Edgefonts.IFontInfoParser {
        public IList<IFontInfo> GenerateFromHtmlTables(string html) {
            XDocument doc = XDocument.Parse(html);
            IList<IFontInfo> fontInfoList = (from e in doc.Descendants("table")
                                             select this.BuildFromHtmlTable(e)).ToList();

            return fontInfoList;
        }

        public IList<IFontInfo> GenerateFromHtmlFile(string filePath) {
            // these files are only ~250k
            string html = File.ReadAllText(filePath);
            return GenerateFromHtmlTables(html);
        }

        public IList<IFontInfo> ReadFontsFromJsonFile(string filePath) {
            if (string.IsNullOrEmpty(filePath)) { throw new ArgumentNullException("filePath"); }

            string jsonContent = File.ReadAllText(filePath);
            return ReadFontsFromJson(jsonContent);
        }

        public IList<IFontInfo> ReadFontsFromJson(string json) {
            if (string.IsNullOrEmpty(json)) { throw new ArgumentNullException("json"); }

            IList<IFontInfo> fontList = null;
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<FontInfo>>(json);

            if (result != null) {
                fontList = result.Cast<IFontInfo>().ToList();
            }

            return fontList;
        }

        public IFontInfo BuildFromHtmlTable(string htmlTable) {
            if (string.IsNullOrEmpty(htmlTable)) { throw new ArgumentNullException("htmlTable"); }

            IFontInfo result = null;

            try {
                XDocument doc = XDocument.Parse(htmlTable);
                result = this.BuildFromHtmlTable(doc.Root);
            }
            catch (XmlException) {
                result = null;
            }

            return result;
        }

        public IFontInfo BuildFromHtmlTable(XElement element) {
            if (element == null) { throw new ArgumentNullException("element"); }

            IFontInfo result = null;
            
            try {
                result = new FontInfo {
                              FamilyDisplayName = element.Element("thead").Element("tr").Element("th").Value,
                              Family = element.Element("thead").Element("tr").Element("td").Element("code").Value,
                              AvailableFontVariations = (from fvd in element.Element("tbody").Elements("tr")
                                                         select new FontVariant {
                                                             VariantName = fvd.Element("th").Value,
                                                             LicenseUri = fvd.Element("td").Elements("a").First().Attribute("href").Value,
                                                             Weight = FontVariant.GetFontWeightFromString(fvd.Element("td").Element("code").Value),
                                                             Style = FontVariant.GetFontStyleFromString(fvd.Element("td").Element("code").Value)
                                                         }).ToList()
                          };
            }
            catch (XmlException) {
                result = null;
            }

            return result;
        }
    }
}
