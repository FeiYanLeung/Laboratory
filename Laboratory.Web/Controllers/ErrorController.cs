using Laboratory.Core.Domain;
using System.Web;
using System.Web.Mvc;

namespace Laboratory.Web.Controllers
{
    /// <summary>
    /// 自定义错误页
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// 500...
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ActionResult Index(CustomExceptionEntity ex)
        {
            Response.StatusCode = 500;
            ViewData = new ViewDataDictionary<CustomExceptionEntity>(ex);

            return View();
        }

        /// <summary>
        /// 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound(string msg)
        {
            Response.StatusCode = 404;
            ViewBag.Msg = HttpUtility.UrlDecode(HttpUtility.HtmlEncode(msg ?? ""));
            return View();
        }
    }
}