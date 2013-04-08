namespace TestEdgefonts {
    using Edgefonts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class FontInfoParserTests {
        [TestClass]
        public class BuildFromHtmlTableTests {
            [TestMethod]
            public void TestFromSimpleHtml01() {
                var htmlString = @"
<table>
  <thead>
    <tr>
      <th>Abel</th>
      <td><code>abel</code></td>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Regular</th>
      <td>
        <code>n4</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121cc"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
  </tbody>
</table>";

                var result = new FontInfoParser().BuildFromHtmlTable(htmlString);
                Assert.IsNotNull(result);
                Assert.AreEqual("Abel", result.FamilyDisplayName);
                Assert.AreEqual("abel", result.Family);
                Assert.AreEqual(1, result.AvailableFontVariations.Count);
                Assert.AreEqual("n4", result.AvailableFontVariations[0].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121cc", result.LicenseUri);
            }
            
        }
    }
}
