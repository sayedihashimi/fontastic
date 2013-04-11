namespace EdgefontsWeb.Controllers.api {
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class FontInfoController : BaseFontController {
        public HttpResponseMessage Get() {
            // this will return all the fonts
            IList<IFontInfo>allFonts = this.NewFontManager().GetFonts();

            return Request.CreateResponse<IList<IFontInfo>>(HttpStatusCode.OK, allFonts);
        }
    }
}
