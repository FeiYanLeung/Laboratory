using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Laboratory.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cs = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
            ViewBag.Title = String.Format("{0} - Home Page", cs.Debug ? "Debug" : "Release");

            return View();
        }
    }
}
