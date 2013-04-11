﻿namespace EdgefontsWeb {
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Linq;

    public class FontManager {
        private const string FontCacheKey = "fontManagerFontList";
        private string fontsFilePath;
        private HttpContextBase Context { get; set; }
        private IFontInfoParser FontParser { get; set; }

        public FontManager(HttpContextBase context,IFontInfoParser fontParser) {
            if (context == null) { throw new ArgumentNullException("context"); }
            if (fontParser == null) { throw new ArgumentNullException("fontParser"); }

            this.Context = context;
            this.FontParser = fontParser;
        }

        public IList<IFontInfo> GetFonts() {
            // see if the font is in the cache or not, if not add it
            IList<IFontInfo> fonts = this.Context.Cache[FontManager.FontCacheKey] as IList<IFontInfo>;

            if (fonts == null) {
                fonts = ReadFontsFromFile();
                // place the fonts in the cache
                this.Context.Cache[FontManager.FontCacheKey] = fonts;
            }

            return fonts;        
        }

        public IList<FontSummary> GetFontNames() {
            var result = (from f in this.GetFonts()
                          select new FontSummary { Family = f.Family, FamilyDisplayName = f.FamilyDisplayName }).ToList();

            return result;
        }

        public string FontsFilePath {
            get {
                if (fontsFilePath == null) {
                    fontsFilePath = System.Configuration.ConfigurationManager.AppSettings["FontsJsFilePath"];
                }
                return fontsFilePath;
            }
            set {
                fontsFilePath = value;
            }
        }

        /// <summary>
        /// This method will read the json file at <code>App_Data\fonts.js</code> and return the font list.
        /// Since this uses IO we should avoid doing this unless needed
        /// </summary>
        private IList<IFontInfo> ReadFontsFromFile() {                        
            string localFilePath = this.Context.Server.MapPath(this.FontsFilePath);

            if (!File.Exists(localFilePath)) {
                throw new FileNotFoundException("The fonts json file was not found at [{0}]", localFilePath);
            }

            return this.FontParser.ReadFontsFromJsonFile(localFilePath);
        }
    }
}