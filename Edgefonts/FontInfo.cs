namespace Edgefonts {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
}
