using AutoMapper;
using System;

namespace Laboratory.Web.Dto
{
    /// <summary>
    /// 当字段类型不一致时，实现接口ITypeConverter可自定义转换
    /// </summary>
    /// <summary>
    /// 自定义字符串和时间类型转换
    /// </summary>
    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
    }
}
