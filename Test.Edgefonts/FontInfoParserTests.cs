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
            public void TestFromSimpleHtml_Abel() {
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
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121cc", result.AvailableFontVariations[0].LicenseUri);
            }

            [TestMethod]
            public void BuildFromHtmlTables_AdventPro() {
                #region html text
                var htmlString = @"
<table>
  <thead>
    <tr>
      <th>Advent Pro</th>
      <td><code>advent-pro</code></td>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Thin</th>
      <td>
        <code>n1</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121b9"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Extra Light</th>
      <td>
        <code>n2</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121b5"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Light</th>
      <td>
        <code>n3</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121bb"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Regular</th>
      <td>
        <code>n4</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121b8"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Medium</th>
      <td>
        <code>n5</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121b6"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Semibold</th>
      <td>
        <code>n6</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121ba"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
    <tr>
      <th>Bold</th>
      <td>
        <code>n7</code>
        <a href=""http://typekit.com/eulas/0000000000000000000121b7"" class=""license-link"" target=""_blank"">font license</a>
      </td>
    </tr>
  </tbody>
</table>
";
                #endregion

                var result = new FontInfoParser().BuildFromHtmlTable(htmlString);
                Assert.IsNotNull(result);
                Assert.AreEqual("Advent Pro", result.FamilyDisplayName);
                Assert.AreEqual("advent-pro", result.Family);

                Assert.AreEqual(7, result.AvailableFontVariations.Count);

                Assert.AreEqual("Thin", result.AvailableFontVariations[0].VariantName);
                Assert.AreEqual("n1", result.AvailableFontVariations[0].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121b9", result.AvailableFontVariations[0].LicenseUri);

                Assert.AreEqual("Extra Light", result.AvailableFontVariations[1].VariantName);
                Assert.AreEqual("n2", result.AvailableFontVariations[1].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121b5", result.AvailableFontVariations[1].LicenseUri);

                Assert.AreEqual("Light", result.AvailableFontVariations[2].VariantName);
                Assert.AreEqual("n3", result.AvailableFontVariations[2].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121bb", result.AvailableFontVariations[2].LicenseUri);

                Assert.AreEqual("Regular", result.AvailableFontVariations[3].VariantName);
                Assert.AreEqual("n4", result.AvailableFontVariations[3].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121b8", result.AvailableFontVariations[3].LicenseUri);

                Assert.AreEqual("Medium", result.AvailableFontVariations[4].VariantName);
                Assert.AreEqual("n5", result.AvailableFontVariations[4].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121b6", result.AvailableFontVariations[4].LicenseUri);

                Assert.AreEqual("Semibold", result.AvailableFontVariations[5].VariantName);
                Assert.AreEqual("n6", result.AvailableFontVariations[5].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121ba", result.AvailableFontVariations[5].LicenseUri);

                Assert.AreEqual("Bold", result.AvailableFontVariations[6].VariantName);
                Assert.AreEqual("n7", result.AvailableFontVariations[6].ToString());
                Assert.AreEqual(@"http://typekit.com/eulas/0000000000000000000121b7", result.AvailableFontVariations[6].LicenseUri);
            }            
        }
    }
}
