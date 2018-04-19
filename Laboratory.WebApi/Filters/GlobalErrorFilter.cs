using Laboratory.WebApi.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Laboratory.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class GlobalErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == true)
            {
                return;
            }
            /*---------------------------------------------------------
             * 日志记录
             ---------------------------------------------------------*/

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;

            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            var exception = new HttpException(filterContext.Exception.Message, filterContext.Exception);
            filterContext.Result = new JsonResult()
            {
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new ResultJson()
                {
                    code = exception.GetHttpCode(),
                    message = exception.Message
                }
            };
        }
    }
}