using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Laboratory.Web.Attributes
{
    /// <summary>
    /// 验证数组和字符串最大长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MaxLengthAttribute : ValidationAttribute, IClientValidatable
    {
        public int Length { get; }
        /// <summary>
        /// 数组或字符串数据的最大允许长度
        /// </summary>
        /// <param name="length"></param>
        public MaxLengthAttribute(int length)
        {
            this.Length = length;
        }

        /// <summary>
        /// 格式化错误信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, name, this.Length);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (null == value) return true;

            var valueType = value.GetType();
            var enumerType = typeof(IEnumerable);
            if (enumerType.IsAssignableFrom(valueType))
            {
                return (value as IEnumerable).Cast<object>().Count() <= this.Length;
            }

            var stringType = typeof(string);
            if (valueType == stringType)
            {
                return value.ToString().Length <= this.Length;
            }

            return true;
        }

        /// <summary>
        /// 注册客户端验证结果信息
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <remarks>
        /// ValidationType可用类型
        /// <see cref="http://jqueryvalidation.org/documentation/#link-list-of-built-in-validation-methods"/>
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var validationRule = new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "maxlength",
            };

            yield return validationRule;
        }
    }
}