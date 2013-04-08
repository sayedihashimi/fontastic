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
                // /table/thead/tr/th
                result.Family = doc.XPathEvaluateElementValue(@"/table/thead/tr/td/code/text()") as string;
                result.FamilyDisplayName = doc.XPathEvaluateElementValue(@"/table/thead/tr/th[1]/text()") as string;

                var fvdNodes = doc.XPathSelectElements(@"/table/tbody/tr/td/code");
                foreach (var fvdNode in fvdNodes) {
                    result.AvailableFontVariations.Add(fvdNode.Value);
                }

                result.LicenseUri = doc.XPathEvaluateAttributeValue(@"/table/tbody/tr/td/a[@class='license-link']/@href") as string;
            }
            catch (XmlException) {
                result = null;
            }

            return result;
        }
    }
}
