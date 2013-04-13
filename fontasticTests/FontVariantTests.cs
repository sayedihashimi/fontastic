namespace FontasticTests {
    using Fontastic;
    using FontasticWeb.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class FontVariantTests {

        [TestClass]
        public class TestWeightSetterMethod {
            [TestMethod]
            public void SetsCorrectlyWhenSingleDigit() {
                FontVariant fv = new FontVariant();

                List<int> valuesToTry = new List<int> {
                    1,2,3,4,5,6,7
                };
                foreach (int i in valuesToTry) {
                    fv.Weight = i;
                    Assert.AreEqual(i, fv.Weight);
                }
            }
            [TestMethod]
            public void SetsCorretlyWhenThreeDigits() {
                FontVariant fv = new FontVariant();

                List<int> valuesToTry = new List<int> {
                    100,200,300,400,500,600,700
                };
                foreach (int valueToTry in valuesToTry) {
                    fv.Weight = valueToTry;
                    Assert.AreEqual(valueToTry/100, fv.Weight);
                }
            }

            [TestMethod]
            public void ThrowsWhenInvalid() {
                FontVariant fv = new FontVariant();

                List<int> invalidValuesToTry = new List<int> {
                    -500,-5555,0,10,125,501,999
                };

                foreach (int valueToTry in invalidValuesToTry) {
                    bool exceptionRaised = false;
                    
                    try {
                        fv.Weight = valueToTry;
                    }
                    catch (InvalidValueException) {
                        exceptionRaised = true;
                    }

                    if (!exceptionRaised) {
                        Assert.Fail(string.Format("The invalid weight value provided [{0}] did not raise an exception as expected",valueToTry));
                    }
                }
            }
        }

        [TestClass]
        public class TestFromStringMethod {
            [TestMethod]
            public void TestFromString() {

                // test normal strings
                for (int i = 1; i <= 7; i++) {
                    string normalFvdStr = string.Format("n{0}", i);
                    Assert.AreEqual(FontStyleEnum.Normal, FontVariant.GetFontStyleFromString(normalFvdStr));
                    Assert.AreEqual(i, FontVariant.GetFontWeightFromString(normalFvdStr));

                    string italicFvdStr = string.Format("i{0}", i);
                    Assert.AreEqual(FontStyleEnum.Italic, FontVariant.GetFontStyleFromString(italicFvdStr));
                    Assert.AreEqual(i, FontVariant.GetFontWeightFromString(italicFvdStr));
                }
            }
            
        }

        [TestClass]
        public class TestToStringMethod {
            [TestMethod]
            public void TestNormalSingleDigit() {                
                Dictionary<FontVariant, string> valueMap = new Dictionary<FontVariant, string>();
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 1 }, "n1");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 2 }, "n2");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 3 }, "n3");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 4 }, "n4");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 5 }, "n5");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 6 }, "n6");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 7 }, "n7");

                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 100 }, "n1");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 200 }, "n2");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 300 }, "n3");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 400 }, "n4");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 500 }, "n5");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 600 }, "n6");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Normal, Weight = 700 }, "n7");

                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 1 }, "i1");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 2 }, "i2");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 3 }, "i3");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 4 }, "i4");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 5 }, "i5");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 6 }, "i6");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 7 }, "i7");

                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 100 }, "i1");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 200 }, "i2");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 300 }, "i3");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 400 }, "i4");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 500 }, "i5");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 600 }, "i6");
                valueMap.Add(new FontVariant { Style = FontStyleEnum.Italic, Weight = 700 }, "i7");

                foreach (FontVariant variant in valueMap.Keys) {
                    Assert.AreEqual(valueMap[variant], variant.ToString());
                }
            }
        }
    }
}
