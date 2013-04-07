namespace Edgefonts {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFontInfo {
        string Family { get; set; }
        string Fallback { get; set; }
        IList<string> AvailableFontVariations { get; set; }
    }

    public class FontInfo : IFontInfo {
        public string Family { get; set; }
        public string Fallback { get; set; }
        public IList<string> AvailableFontVariations { get; set; }
    }
}
