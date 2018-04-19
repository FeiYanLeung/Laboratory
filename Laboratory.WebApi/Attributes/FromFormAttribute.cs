using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;
using System.Web.ModelBinding;

namespace Laboratory.WebApi.Attributes
{
    /// <summary>
    /// 一个特性，该特性指定操作参数仅来自传入 System.Net.Http.HttpRequestMessage 的实体正文。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromFormAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// This attribute is used on action parameters to indicate
        /// they come only from the content body of the incoming <see cref="HttpRequestMessage"/>.
        /// </summary>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            IEnumerable<MediaTypeFormatter> formatters = parameter.Configuration.Formatters;
            IBodyModelValidator validator = parameter.Configuration.Services.GetBodyModelValidator();

            return parameter.BindWithFormatter(formatters, validator);
        }
    }
}