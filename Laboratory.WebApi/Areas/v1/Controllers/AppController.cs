using System.Web.Mvc;

namespace Laboratory.WebApi.Areas.v1.Controllers
{

    public class AppController : Controller
    {
        // GET: vv/app
        // Res: Error
        [HttpGet]
        public JsonResult Version()
        {
            int a = 0;
            int b = 1 - 1;

            return Json(a / b);
        }
    }
}