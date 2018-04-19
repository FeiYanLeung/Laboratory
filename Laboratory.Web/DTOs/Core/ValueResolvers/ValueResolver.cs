using AutoMapper;
using System;

namespace Laboratory.Web.Dto
{
    /**
     * 自定义实体值的转换，适用于目标与源实体直接的值类型计算，参考
     * https://github.com/AutoMapper/AutoMapper/wiki/Custom-value-resolvers
     */
    public class ValueResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, int>
    {
        /// <summary>
        /// 自定义实体值的转换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public int Resolve(TSource source, TDestination destination, int destMember, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
