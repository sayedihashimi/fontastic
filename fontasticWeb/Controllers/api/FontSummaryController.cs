namespace FontasticWeb.Controllers.api
{
    using Fontastic;
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
        /// <summary>
        /// This returns the font name and display name.
        /// You can use OData to query this. Examples:
        /// - How to get fonts that start with 'ab'
        ///     http://localhost:29284/o/FontSummary?$filter=startswith(Family,'ab')%20eq%20true
        /// </summary>
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
