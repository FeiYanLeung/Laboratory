using System.Web.Mvc;
using System.Web.Routing;

namespace Laboratory.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Culture",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { culture = "zh-CN", controller = "Default", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
