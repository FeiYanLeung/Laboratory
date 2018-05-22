using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Laboratory.Web.Attributes
{
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        private const string CULTURE_ID = "culture";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var cookies = filterContext.HttpContext.Request.Cookies;
                var culture = filterContext.RouteData.Values[CULTURE_ID];
                var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                if (culture == null && cookies.AllKeys.Any(w => string.Equals(CULTURE_ID, w)))
                {
                    culture = cookies[CULTURE_ID].Value;
                }

                if (culture != null && cultures.Any(w => string.Equals(culture.ToString(), w.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    var cultureInfo = CultureInfo.CreateSpecificCulture(culture.ToString());
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;

                    filterContext.HttpContext.Response.SetCookie(new HttpCookie(CULTURE_ID, culture.ToString())
                    {
                        Expires = DateTime.Now.AddMonths(1),
                        HttpOnly = true
                    });
                }
            }
            catch (Exception)
            {
            }
            base.OnActionExecuting(filterContext);
        }
    }
}