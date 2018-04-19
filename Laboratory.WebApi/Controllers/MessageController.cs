using System.Web.Http;

namespace Laboratory.WebApi.Controllers
{
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        [Route("send"), HttpPost]
        public string Send()
        {
            return "successful";
        }
    }
}
