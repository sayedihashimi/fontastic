namespace EdgefontsWeb.Controllers {
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller {    
        public ActionResult Index() {
            return View();
        }

        public ActionResult GetAllFonts() {
            var fontList = this.GetFontManager().GetFonts();

            return Json(fontList, JsonRequestBehavior.AllowGet);       
        }

        public virtual FontManager GetFontManager() {
            // TODO: Could pass this in via IOC
            return new FontManager(this.HttpContext, new FontInfoParser());
        }
    }
}
