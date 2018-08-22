// 标记是否是发布环境
#define OFFLINE

using Laboratory.Web.Dto;
using System;
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

        /// <summary>
        /// 程序4x错误处理
        /// 4x错误(业务)在NotFoundResult中处理
        /// 5x错误在CustomErrorAttribute中处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                var httpException = exception as HttpException;
                if (httpException != null)
                {
                    int httpCode = httpException.GetHttpCode();

                    // 待处理的错误状态码
                    var errorCodes = new int[] { 400, 404 };

                    if (Array.IndexOf(errorCodes, httpCode) > -1)
                    {
                        // 清除异常。
                        Server.ClearError();

                        // 尝试禁用IIS自定义错误
                        Response.TrySkipIisCustomErrors = true;

#if ONLINE
                        Response.Redirect("~/NotFound.html");
                        return;
#endif
                        var routeData = new RouteData();
                        routeData.Values["controller"] = "Error";
                        routeData.Values["action"] = "NotFound";
                        routeData.Values["error"] = httpException;

                        var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
#if DEBUG
                        var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
                        var errorController = controllerFactory.CreateController(requestContext, "Error");
#else
                        var errorController  = new ErrorController();
#endif

                        // 调用并传递routeData到目标Controller
                        errorController.Execute(requestContext);
                    }
                }
            }
        }
    }
}
