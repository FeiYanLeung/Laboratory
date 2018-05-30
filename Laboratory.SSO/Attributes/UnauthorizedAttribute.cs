using Microsoft.AspNetCore.Authorization;
using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Laboratory.SSO
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class UnauthorizedAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var regexQut = new Regex(@"[&|\?]token=\w+", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            var returnUrl = regexQut.Replace(filterContext.HttpContext.Request.Url.AbsoluteUri ?? "", "");
            var regexUri = new Regex(@"^((https|http|ftp|rtsp|mms)?:\/\/)[^\s]+", RegexOptions.IgnoreCase);

            if (regexUri.IsMatch(returnUrl))
            {
                var returnUri = new Uri(returnUrl);
                if (returnUri.Host.EndsWith(FormsAuthentication.CookieDomain, StringComparison.OrdinalIgnoreCase))
                {
                    returnUrl = returnUri.AbsolutePath;
                }
            }

            filterContext.Result = new RedirectResult(String.Concat(FormsAuthentication.LoginUrl, "?returnUrl=", HttpUtility.UrlEncode(returnUrl)));
        }
    }
}