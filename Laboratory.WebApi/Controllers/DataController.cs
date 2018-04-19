using Laboratory.WebApi.Models.Parameters;
using System.Web.Http;

namespace Laboratory.WebApi.Controllers
{
    /// <summary>
    /// 使用Ajax请求时，前台的参数需要使用JSON.stringify(parameter)转换一次
    /// </summary>
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        /// <summary>
        /// 复杂类型请求
        /// <![CDATA[
        /// 自定义类型，请求时Content-Type="application/json",内容为JsonObject
        /// ]]>
        /// </summary>
        /// <param name="parameter">Complex Types</param>
        /// <returns></returns>
        [Route("complex"), HttpPost]
        public DataParameter PostComplexData([FromBody]DataParameter parameter)
        {
            return parameter;
        }

        /// <summary>
        /// 简单类型请求
        /// <![CDATA[
        /// 简单数据类型，请求时Content-Type="application/json",内容为Json字符串
        /// primitive types:[int, bool, double, and so forth]
        /// extra types:[TimeSpan, DateTime, Guid, decimal, and string]
        /// ]]>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Route("simple"), HttpPost]
        public string PostSimpleData([FromBody] string parameter)
        {
            return parameter;
        }
    }
}
