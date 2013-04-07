namespace Edgefonts {
    using Edgefonts.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class FontVariant {
        private int weight;

        public FontStyleEnum Style { get; private set; }
        public int Weight { 
            get { return weight; }
            internal set {
                // can be either 1,2,3,4,5,6,7 or 100,200,300,400,500,600,700
                string pattern = "^[1-9]{1}(00)?$";
                if (!Regex.IsMatch(value.ToString(), pattern)) {
                    throw new InvalidValueException(
                        string.Format("The weight value provided [{0}] is invalid", value));
                }

                // this weight value will always be one of 1,2,3,4,5,6,7
                this.weight = value <= 7 ? value : value / 100;
            }
        }

        public override string ToString() {
            string collapsed = string.Format("{0}{1}",
                this.Style == FontStyleEnum.Normal?"n":"i",
                this.Weight.ToString());

            return collapsed;
        }

        public static FontVariant FromString(string fvdString) {
            if (string.IsNullOrEmpty(fvdString)) { throw new ArgumentNullException("fvdString"); }

            // should look like n1,n2,...,n7 or i1,i2,...,i7
            string pattern = "^(n[1-9]|i[1-9])$";
            if (!Regex.IsMatch(fvdString, pattern)) {
                throw new InvalidValueException(
                    string.Format("The provided fvdString [{0}] is invalid.",fvdString));
            }

            return new FontVariant {
                Style = GetFontStyleFromString(fvdString),
                Weight = GetFontWeightFromString(fvdString)
            };
        }
        /// <summary>
        /// Get's the font style from the fvd string.
        /// </summary>
        /// <param name="fvdString">Should look like n1,n2,...,n7 or i1,i2,...,i7 </param>
        private static FontStyleEnum GetFontStyleFromString(string fvdString) {
            if (string.IsNullOrEmpty(fvdString)) { throw new ArgumentNullException("fvdString"); }

            FontStyleEnum result = FontStyleEnum.Normal;
            fvdString = fvdString.Trim();

            if (fvdString.StartsWith("n", StringComparison.OrdinalIgnoreCase)) {
                result = FontStyleEnum.Normal;
            }
            else if (fvdString.StartsWith("i", StringComparison.OrdinalIgnoreCase)) {
                result = FontStyleEnum.Italic;
            }
            else {
                throw new InvalidValueException(
                    string.Format("The provided fvdString [{0}] does not have a valid style value.", fvdString));
            }

            return result;
        }
        /// <summary>
        /// Get's the font weight from the fvd string
        /// </summary>
        /// <param name="fvdString">Should look like n1,n2,...,n7 or i1,i2,...,i7 </param>
        private static int GetFontWeightFromString(string fvdString) {
            if (string.IsNullOrEmpty(fvdString)) { throw new ArgumentNullException("fvdString"); }
            fvdString = fvdString.Trim();

            int weight = 1;
            try {
                weight = int.Parse(fvdString.Substring(1, 1));
                if ( weight < 1 || weight > 7) {
                    throw new InvalidValueException(string.Format("The font weight provided [{0}] is invalid", weight));
                }
            }
            catch (Exception ex) {
                throw new InvalidValueException(
                    string.Format("The provided fvdString [{0}] does not have a valid font weight value.", fvdString));
            }

            return weight;
        }
    }

    public enum FontStyleEnum {
        Normal,
        Italic
    }    
}
