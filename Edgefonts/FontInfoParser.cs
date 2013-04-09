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
            throw new NotImplementedException();
        }

        public IFontInfo BuildFromHtmlTable(string htmlTable) {
            if (string.IsNullOrEmpty(htmlTable)) { throw new ArgumentNullException("htmlTable"); }

            IFontInfo result = new FontInfo();

            try {
                XDocument doc = XDocument.Parse(htmlTable);
                result = (from e in doc.Elements("table")
                          select new FontInfo {
                              FamilyDisplayName = e.Element("thead").Element("tr").Element("th").Value,
                              Family = e.Element("thead").Element("tr").Element("td").Element("code").Value,
                              AvailableFontVariations = (from fvd in e.Element("tbody").Elements("tr")
                                                         select new FontVariant{
                                                             VariantName = fvd.Element("th").Value,
                                                             LicenseUri = fvd.Element("td").Elements("a").First().Attribute("href").Value,
                                                             Weight = FontVariant.GetFontWeightFromString(fvd.Element("td").Element("code").Value),
                                                             Style = FontVariant.GetFontStyleFromString(fvd.Element("td").Element("code").Value)
                                                         }).ToList()
                          }).Single();
            }
            catch (XmlException) {
                result = null;
            }

            return result;
        }
    }
}
