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

    public abstract class BaseFontController : ApiController
    {
        protected virtual FontManager NewFontManager() {
            // TODO: Could pass this in via IOC            

            // System.Web.HttpContext.Current
            HttpContextBase ctxBase = new System.Web.HttpContextWrapper(System.Web.HttpContext.Current);

            return new FontManager(ctxBase, new FontInfoParser());
        }
    }
}
