<Query Kind="Program" />

void Main()
{
		
		
	var htmlTable = @"
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
	
	IFontInfo result = new FontInfo();
	XDocument doc = XDocument.Parse(htmlTable);
                result = (from e in doc.Elements("table")
                          select new FontInfo {
                              FamilyDisplayName = e.Element("thead").Element("tr").Element("th").Value,
                              Family = e.Element("thead").Element("tr").Element("td").Element("code").Value,
                              AvailableFontVariations = (from fvd in e.Element("tbody").Elements("tr")
                                                         select new FontVariant{
                                                             VariantName = fvd.Element("th").Value,
                                                             LicenseUri = fvd.Element("td").Elements("a").First().Value,
                                                             Weight = FontVariant.FromString(fvd.Element("td").Element("code").Value).Weight,
                                                             Style = FontVariant.FromString(fvd.Element("td").Element("code").Value).Style
                                                         }).ToList()
                          }).Single();
			  
	result.Dump();
	}

// Define other methods and classes here

    public interface IFontInfo {
        string FamilyDisplayName { get; set; }
        string Family { get; set; }
        string Fallback { get; set; }
        IList<FontVariant> AvailableFontVariations { get; set; }
        // string LicenseUri { get; set; }
    }

    public class FontInfo : IFontInfo {
        public FontInfo() {
            this.AvailableFontVariations = new List<FontVariant>();
        }

        public string FamilyDisplayName { get; set; }
        public string Family { get; set; }
        public string Fallback { get; set; }
        public IList<FontVariant> AvailableFontVariations { get; set; }
        // public string LicenseUri { get; set; }
    }

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
	[Serializable]
    public class InvalidValueException : Exception {
        public InvalidValueException() { }
        public InvalidValueException(string message) : base(message) { }
        public InvalidValueException(string message, Exception inner) : base(message, inner) { }
        protected InvalidValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }