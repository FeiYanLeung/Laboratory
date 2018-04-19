using System.IO;
using System.Web.Mvc;

namespace Laboratory.Web.Controllers
{
    /// <summary>
    /// 微信公众号测试
    /// </summary>
    public class WeChatController : Controller
    {
        /// <summary>
        /// 模板消息发送完成后，微信服务器送达结果通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Receive()
        {
            using (var sr = new StreamReader(Request.InputStream))
            {
                var requestInput = sr.ReadToEnd();



            }

            return View();
        }
    }
}