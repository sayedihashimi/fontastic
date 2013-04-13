namespace Fontastic {
    using Edgefonts.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class FontVariant {
        private int weight;

        /// <summary>
        /// For example Regular,Thin,Extra Light, Italic, etc
        /// </summary>
        public string VariantName { get; set; }
        public string LicenseUri { get; set; }
        public FontStyleEnum Style { get; set; }
        public int Weight { 
            get { return weight; }
            set {
                // can be either 1,2,3,4,5,6,7,8,9 or 100,200,300,400,500,600,700,800,900
                string pattern = "^[1-9]{1}(00)?$";
                if (!Regex.IsMatch(value.ToString(), pattern)) {
                    throw new InvalidValueException(
                        string.Format("The weight value provided [{0}] is invalid", value));
                }

                // this weight value will always be one of 1,2,3,4,5,6,7,8,9
                this.weight = (value <= 9 ? value : value / 100);
            }
        }

        public override string ToString() {
            string collapsed = string.Format("{0}{1}",
                this.Style == FontStyleEnum.Normal?"n":"i",
                this.Weight.ToString());

            return collapsed;
        }


        private static void ValidateFvdString(string fvdString){
            // should look like n1,n2,...,n7 or i1,i2,...,i7
            string pattern = "^(n[1-9]|i[1-9])$";
            if (string.IsNullOrWhiteSpace(fvdString) || !Regex.IsMatch(fvdString, pattern)) {
                throw new InvalidValueException(
                    string.Format("The provided fvdString [{0}] is invalid.",fvdString));
            }
        }

        /// <summary>
        /// Get's the font style from the fvd string.
        /// </summary>
        /// <param name="fvdString">Should look like n1,n2,...,n9 or i1,i2,...,i9 </param>
        public static FontStyleEnum GetFontStyleFromString(string fvdString) {
            ValidateFvdString(fvdString);

            FontStyleEnum result = FontStyleEnum.Normal;
            fvdString = fvdString.Trim();

            if (fvdString.StartsWith("n", System.StringComparison.OrdinalIgnoreCase)) {
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
        /// <param name="fvdString">Should look like n1,n2,...,n0 or i1,i2,...,i9 </param>
        public static int GetFontWeightFromString(string fvdString) {
            ValidateFvdString(fvdString);

            fvdString = fvdString.Trim();

            int weight = 1;
            try {
                weight = int.Parse(fvdString.Substring(1, 1));
                if ( weight < 1 || weight > 9) {
                    throw new InvalidValueException(string.Format("The font weight provided [{0}] is invalid", weight));
                }
            }
            catch (Exception) {
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
