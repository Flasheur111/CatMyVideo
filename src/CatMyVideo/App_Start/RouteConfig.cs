﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatMyVideo
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
         name: "Account",
         url: "Account/{action}/{nickname}",
         defaults: new { controller = "Account", action = "Display" }
      );

      routes.MapRoute(
        name: "Video",
        url: "Video/{action}/{id}",
        defaults: new { controller = "Video", action = "Display", id = UrlParameter.Optional }
      );

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      );

      routes.MapRoute(
          name: "DefaultVideo",
          url: "api/{controller}/{action}/{id}"
      );
    }
  }
}
