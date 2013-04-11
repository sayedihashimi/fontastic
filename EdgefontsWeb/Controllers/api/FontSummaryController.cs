namespace EdgefontsWeb.Controllers.api
{
    using Edgefonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.OData;
    using System.Web.Http.OData.Query;

    public class FontSummaryController : EntitySetController<FontSummary,string>
    {
        [Queryable(PageSize=50,AllowedQueryOptions=AllowedQueryOptions.All)]
        public override IQueryable<FontSummary> Get() {
            var result = (from f in this.NewFontManager().GetFonts()
                          select new FontSummary { Family = f.Family, FamilyDisplayName = f.FamilyDisplayName });

            return result.AsQueryable();
        }

        public override HttpResponseMessage Get(string key) {
            var fontSummary = (from f in this.NewFontManager().GetFonts()
                               where string.Compare(f.Family, key, StringComparison.OrdinalIgnoreCase) == 0
                               select f).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, fontSummary);
        }
    }
}
