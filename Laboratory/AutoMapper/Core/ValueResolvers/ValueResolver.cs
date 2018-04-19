using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory.AutoMapper
{
    /**
     * 指定实体中值的转换
     * <seealso cref="https://github.com/AutoMapper/AutoMapper/wiki/Custom-value-resolvers"/>
     */
    public sealed class ValueResolver : IValueResolver<SourceDto, Source, ICollection<int>>
    {
        /// <summary>
        /// 解析Core
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public ICollection<int> Resolve(SourceDto source, Source destination, ICollection<int> destMember, ResolutionContext context)
        {
            return source.tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => int.TryParse(w, out int tag_id))
                .Select(q => int.Parse(q))
                .ToList();
        }
    }
}
