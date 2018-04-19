using Laboratory.Web.Dto;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Laboratory.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mappings.Register();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (ex is HttpException)
            {
                var httpCode = ((HttpException)ex).GetHttpCode();

                if (httpCode == 400 || httpCode == 404)
                {
                    Server.ClearError();
                    Response.Clear();

                    Response.StatusCode = 404;
                    Response.Redirect("/NotFound.html");
                }
            }
        }
    }
}
