using Laboratory.Core;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Laboratory.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                var requestUri = filterContext.HttpContext.Request.Url;
                var domain = AppConfig.DOMAIN_SUFFIX;
                var domainUri = new Uri(domain);

                //当前请求路径为设置的登录地址
                if (requestUri.AbsolutePath.Equals(FormsAuthentication.LoginUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                //如果能够匹配设置的域名Host，则表示在本站内跳转。否则需要注意CSRF
                if (requestUri.Host.Equals(domainUri.Host, StringComparison.CurrentCultureIgnoreCase) || domainUri.Host.EndsWith(requestUri.Host, StringComparison.CurrentCultureIgnoreCase))
                {
                    filterContext.Result = RedirectToAction("Login", "Home", new { returnUrl = HttpUtility.UrlEncode(requestUri.AbsoluteUri) });
                }
                else
                {
                    var returnUrl = HttpUtility.UrlEncode(Request["ReturnUrl"]);
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        filterContext.Result = RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        filterContext.Result = RedirectToAction("Login", "Home", new { returnUrl = returnUrl });
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

    }
}