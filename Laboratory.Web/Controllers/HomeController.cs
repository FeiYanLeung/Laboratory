using Laboratory.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Laboratory.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var dire = new DirectoryInfo(Server.MapPath("/Content/avatars"));
            if (dire.Exists)
            {
                var extFilter = new List<string>()
                {
                    ".png",".jpg",".bmp",".gif",".jpeg"
                };
                var avatars = dire.GetFiles().Where(q => extFilter.Contains(q.Extension.ToLower()))
                       .ToList();

                if (avatars.Any())
                {

                }
            }

            ViewBag.Output = HttpUtility.HtmlEncode("&#60;scr-->ipt>alert('bingo')&#60;/scr-->ipt>".DropHTML()).Trim();

            //  throw new HttpException("Error!");
            return View();
        }

        public JsonResult SetCookie()
        {
            var cookie = new HttpCookie("uid", "10001")
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true
            };

            Response.Cookies.Add(cookie);

            return Json(new { code = 0 });
        }

        public JsonResult Logout()
        {
            var cookie = new HttpCookie("uid", "10001")
            {
                Expires = DateTime.Now.AddHours(-2)
            };
            Response.Cookies.Remove("uid");
            Response.Cookies.Add(cookie);
            return Json(new { code = 0 });
        }

        [Authorize]
        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";
            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password, string returnUrl = "")
        {
            if ("admin".Equals(name) && "123456".Equals(password))
            {
                FormsAuthentication.SetAuthCookie(name, true);

                #region 登录成功跳转

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    var redirectUrl = HttpUtility.UrlDecode(returnUrl);
                    var regexUri = new Regex(@"^((https|http|ftp|rtsp|mms)?:\/\/)[^\s]+", RegexOptions.IgnoreCase);

                    if (regexUri.IsMatch(redirectUrl))
                    {
                        var redirectUri = new Uri(redirectUrl);

                        var domain = AppConfig.DOMAIN_SUFFIX;
                        var domainUri = new Uri(domain);

                        //当前请求路径为设置的登录地址
                        if (redirectUri.AbsolutePath.Equals(FormsAuthentication.LoginUrl, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (domainUri.Host.Equals(redirectUri.Host))
                        {
                            return Redirect(redirectUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return Redirect(redirectUrl);
                    }
                }

                #endregion
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }


        public ActionResult SignOut()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                ViewBag.AuthMsg = string.Format("{0},我们下次见。", HttpContext.User.Identity.Name);
            }
            else
            {
                ViewBag.AuthMsg = "你还未登陆呢。";
            }

            return View("Login");
        }
    }
}