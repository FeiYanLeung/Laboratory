using System.Net;

namespace Laboratory.NetCore.Web.Models
{
    public class ResultData
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }

    public class ResultData<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
