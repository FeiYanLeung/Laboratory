using Laboratory.Core.Domain;
using System.Web.Mvc;

namespace Laboratory.Web.Controllers
{
    /// <summary>
    /// 自定义错误500
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// 500
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Index(ErrorMessage ex)
        {
            ViewData = new ViewDataDictionary<ErrorMessage>(ex);
            return View();
        }
    }
}