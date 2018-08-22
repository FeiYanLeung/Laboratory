using Laboratory.Core.Domain;
using Laboratory.Core.Enums;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Laboratory.Web.Attributes
{
    public class CustomErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 输出格式
        /// </summary>
        public EnumOutputFormat Format;

        public CustomErrorAttribute()
        {
            Format = EnumOutputFormat.Redirect;
        }
        /// <summary>
        /// 相应格式(跳转或JSON)
        /// </summary>
        /// <param name="format"></param>
        public CustomErrorAttribute(EnumOutputFormat format)
        {
            this.Format = format;
        }

        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) return;

            #region 检测ActionResult返回类型

            #region 检测是否已经在Action或Controller上设置 CustomErrorAttribute

            Type customErrorAttributeType = typeof(CustomErrorAttribute);

            object[] attributes = null;

            if (filterContext.ActionDescriptor.IsDefined(customErrorAttributeType, true))
            {
                attributes = filterContext.ActionDescriptor.GetCustomAttributes(customErrorAttributeType, true);
            }
            else if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(customErrorAttributeType, true))
            {
                attributes = filterContext.ActionDescriptor.GetCustomAttributes(customErrorAttributeType, true);
            }

            if (attributes == null)
            {
                #region 如果没有设置CustomErrorAttribute,则使用最终的相应格式输出数据

                var actionDescriptor = filterContext.ActionDescriptor as IMethodInfoActionDescriptor;
                var actionReturnType = actionDescriptor.MethodInfo.ReturnType;

                if (object.ReferenceEquals(typeof(JsonResult), actionReturnType)) this.Format = EnumOutputFormat.JSON;
                else this.Format = EnumOutputFormat.Redirect;

                #endregion
            }
            else
            {
                this.Format = ((CustomErrorAttribute)attributes.Last()).Format;
            }

            #endregion

            #endregion

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            switch (this.Format)
            {
                case EnumOutputFormat.Redirect:
                    {
                        var ex = new CustomExceptionEntity(filterContext.Exception);
                        var request = filterContext.HttpContext.Request;
                        ex.IISVersion = request.ServerVariables["SERVER_SOFTWARE"];
                        ex.UserAgent = request.UserAgent;
                        ex.Path = request.Path;
                        ex.HttpMethod = request.HttpMethod;

                        filterContext.Result = new ViewResult()
                        {
                            ViewName = "~/Views/Error/Index.cshtml",
                            ViewData = new ViewDataDictionary<CustomExceptionEntity>(ex)
                        };
                    }
                    break;
                case EnumOutputFormat.JSON:
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = new
                            {
                                code = HttpStatusCode.InternalServerError,
                                message = filterContext.Exception.Message
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    break;
            }
            filterContext.ExceptionHandled = true;
        }
    }
}
