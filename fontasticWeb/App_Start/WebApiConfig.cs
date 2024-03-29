﻿using Fontastic;
using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Formatter;

namespace FontasticWeb {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<FontInfo>("FontInfo");
            modelBuilder.EntitySet<FontSummary>("FontSummary");
            IEdmModel model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute(routeName: "odataFontInfo", routePrefix: "o", model: model);

            //ODataConventionModelBuilder fsBuilder = new ODataConventionModelBuilder();
            //fsBuilder.EntitySet<FontSummary>("FontSummary");
            //IEdmModel fsModel = fsBuilder.GetEdmModel();
            //config.Routes.MapODataRoute(routeName: "routeTwo", routePrefix: "o2", model: fsModel);


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();
            config.Formatters.InsertRange(0, ODataMediaTypeFormatters.Create());

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
