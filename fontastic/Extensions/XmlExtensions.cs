namespace FontasticWeb.Extensions {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public static class XmlExtensions {
        public static string XPathEvaluateElementValue(this XDocument doc, string expression) {
            if (string.IsNullOrEmpty(expression)) { throw new ArgumentNullException("expression"); }

            string result = null;
            IEnumerable queryResult = doc.XPathEvaluate(expression) as IEnumerable;
            if (queryResult != null) {
                result = queryResult.Cast<XText>().FirstOrDefault().Value;
            }

            return result;
        }

        public static string XPathEvaluateAttributeValue(this XDocument doc, string expression) {
            if (string.IsNullOrEmpty(expression)) { throw new ArgumentNullException("expression"); }

            string result = null;
            IEnumerable queryResult = doc.XPathEvaluate(expression) as IEnumerable;
            if (queryResult != null) {
                result = queryResult.Cast<XAttribute>().FirstOrDefault().Value;
            }

            return result;
        }
    }
}
