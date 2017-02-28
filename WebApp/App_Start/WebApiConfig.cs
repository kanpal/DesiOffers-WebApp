using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public static class WebApiConfig
    {
        private static readonly string[] ControllerNamespaces = new[] { "WebApp.Controllers" };


        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Account API
/*            config.Routes.MapHttpRoute(
                name: "AccountApiLogon",
                routeTemplate: "api/account/logon",
                defaults: new { controller = "Account", action = "ApiLogon" }
            );
*/
            // Customer API
            config.Routes.MapHttpRoute(
                name: "CustomerApi",
                routeTemplate: "api/customer/{action}/{id}",
                defaults: new { controller = "customerapi", action = "Get", id = RouteParameter.Optional }                
            );

            // Product API
            config.Routes.MapHttpRoute(
                name: "ProductApi",
                routeTemplate: "api/product/{action}/{id}",
                defaults: new { controller = "ProductApi", action = "Get", id = RouteParameter.Optional }
            );

            // Image API
            config.Routes.MapHttpRoute(
                name: "ImageApi",
                routeTemplate: "api/image/{action}/{id}",
                defaults: new { controller = "ImageApi", action = "Get", id = RouteParameter.Optional }
            );

            // Default API Route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

        public static void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                    "AccountApiLogon",
                    "api/account/logon",
                    new { controller = "Account", action = "ApiLogon" },
                    ControllerNamespaces
            );
        }
    }
}
