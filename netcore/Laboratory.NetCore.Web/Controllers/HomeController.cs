using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Laboratory.NetCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _log;
        public HomeController(ILogger<HomeController> log)
        {
            this._log = log;
        }
        public IActionResult Index()
        {
            this._log.LogError("welcome！");
            return View();
        }
    }
}
