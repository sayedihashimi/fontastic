namespace Edgefonts {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Edgefonts.Extensions;

    public class FontInfoParser {
        public IList<IFontInfo> GenerateFromHtmlTables(string html) {
            XDocument doc = XDocument.Parse(html);
            IList<IFontInfo> fontInfoList = (from e in doc.Descendants("table")
                                             select this.BuildFromHtmlTable(e)).ToList();

            return fontInfoList;
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
