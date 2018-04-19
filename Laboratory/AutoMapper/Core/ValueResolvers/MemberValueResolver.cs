using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory.AutoMapper
{
    /// <summary>
    /// 通过指定属性来转换
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public sealed class MemberValueResolver : IMemberValueResolver<object, object, ICollection<string>, ICollection<SourceItem>>
    {
        public ICollection<SourceItem> Resolve(object source, object destination, ICollection<string> sourceMember, ICollection<SourceItem> destMember, ResolutionContext context)
        {
            return sourceMember.Select(q => new SourceItem
            {
                Title = q
            })
            .ToList();
        }
    }
}
