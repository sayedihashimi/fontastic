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
        /// To get the top 10 results: /api/FontInfo?$top=10
        /// To get the abel font:      /api/FontInfo?$filter=Family%20eq%20'abel'
        /// </summary>
        /// <returns></returns>
        [Queryable(PageSize = 10)]
        public IQueryable<IFontInfo> Get() {
            var result = from f in this.NewFontManager().GetFonts()
                         select f;

            return result.AsQueryable();
        }
    }
}
