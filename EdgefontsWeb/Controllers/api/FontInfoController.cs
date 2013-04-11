namespace EdgefontsWeb.Controllers.api {
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class FontInfoController : BaseFontController {
        /// <summary>
        /// Since this is IQueryable you can make OData calls to it.
        /// To get all the fonts (1 page at a time):    /o/FontInfo
        /// To get the top 10 results:                  /o/FontInfo?$top=2
        /// To get the abel font:                       /o/FontInfo?$filter=Family%20eq%20'abel'
        /// To get a particular font:                   /o/FontInfo(abel)
        /// </summary>
        /// <returns></returns>
        [Queryable(PageSize = 10)]
        public override IQueryable<FontInfo> Get() {
            var result = from f in this.NewFontManager().GetFonts()
                         select f;

            return result.AsQueryable();
        }

        public override HttpResponseMessage Get(string key) {
            var font = (from f in this.NewFontManager().GetFonts()
                        where string.Compare(f.Family,key,StringComparison.OrdinalIgnoreCase)==0
                        select f).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, font);
        }
    }
}
