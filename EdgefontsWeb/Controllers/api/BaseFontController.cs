namespace EdgefontsWeb.Controllers.api
{
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.OData;

    public abstract class BaseFontController : EntitySetController<FontInfo,string>
    {
        protected virtual FontManager NewFontManager() {
            // TODO: Could pass this in via IOC            
            HttpContextBase ctxBase = new System.Web.HttpContextWrapper(System.Web.HttpContext.Current);

            return new FontManager(ctxBase, new FontInfoParser());
        }
    }
}
