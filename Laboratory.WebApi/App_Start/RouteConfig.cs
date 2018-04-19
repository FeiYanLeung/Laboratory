using System.Web.Mvc;
using System.Web.Routing;

namespace Laboratory.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "dynamic_router",
                url: "{version}/{controller}/{action}/{id}",
                defaults: new { version = "v3", id = UrlParameter.Optional }
            );
        }
    }
}
