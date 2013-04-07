namespace TestEdgefonts {
    using Edgefonts;
    using Edgefonts.Exceptions;
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
                    var normalFv = FontVariant.FromString(normalFvdStr);
                    Assert.AreEqual(FontStyleEnum.Normal, normalFv.Style);
                    Assert.AreEqual(i, normalFv.Weight);

                    string italicFvdStr = string.Format("i{0}", i);
                    var italicFv = FontVariant.FromString(italicFvdStr);
                    Assert.AreEqual(FontStyleEnum.Italic, italicFv.Style);
                    Assert.AreEqual(i, italicFv.Weight);
                }
            }
            
        }

        [TestClass]
        public class TestToStringMethod {
            [TestMethod]
            public void TestNormalSingleDigit() {
                List<string> variantsToTest = new List<string>();

                // add all normal variants
                for (int i = 1; i < 7; i++) {
                    variantsToTest.Add(string.Format("n{0}", i));
                }

                // add all italic variants
                for (int i = 1; i < 7; i++) {                    
                    variantsToTest.Add(string.Format("i{0}", i));
                }

                foreach (string varToTest in variantsToTest) {
                    var fv = FontVariant.FromString(varToTest);
                    Assert.AreEqual(varToTest, varToTest.ToString());
                }
            }
        }
    }
}
