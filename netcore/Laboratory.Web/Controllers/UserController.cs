using Laboratory.NetCore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Laboratory.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return Json(new ResultData()
            {
                Code = HttpStatusCode.OK,
                Message = "ok"
            });
        }

        public IActionResult GetValues()
        {
            return Json(new ResultData()
            {
                Code = HttpStatusCode.OK,
                Message = "ok"
            });
        }
    }
}