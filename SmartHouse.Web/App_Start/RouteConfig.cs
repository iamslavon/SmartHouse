﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SmartHouse.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "",
                "data/get",
                new { controller = "Data", action = "GetData"}
            );

            routes.MapRoute(
                "",
                "data/set",
                new { controller = "Data", action = "SetData" }
            );

            routes.MapRoute(
                "",
                "error",
                new { controller = "Error", action = "Default" }
            );

            routes.MapRoute(
                "",
                "404",
                new { controller = "Error", action = "NotFound" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
