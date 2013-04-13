using Fontastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FontasticWeb.Controllers.api {
    public static class ControllerExtensions {

        public static FontManager NewFontManager(this ApiController controller){
            return new FontManager(new System.Web.HttpContextWrapper(System.Web.HttpContext.Current),
                new FontInfoParser());
        }
    }
}