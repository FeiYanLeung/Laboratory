
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory.AutoMapper
{
    public class SourceProfile : Profile, IProfile
    {
        public SourceProfile()
        {
            //base.CreateMap<Source, SourceDto>()
            //    .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.text_length, opts => opts.ResolveUsing<ValueResolver>())
            //    .ForMember(dest => dest.title, opts => opts.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.source_items, opts => opts.MapFrom(src => src.SourceItems))
            //    .SkipVirtualProperties()
            //    .SetWithinProperties(src => src.Title, src => src.Title)
            //    .SetWithoutProperties(src => src.Title)
            //    .ReverseMap();

            base.CreateMap<Source, SourceDto>()
                .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.tags, opts =>
                {
                    opts.Condition(w => w.Tags.Any());
                    opts.ResolveUsing<string>((src, dest) =>
                    {
                        return string.Join(",", src.Tags);
                    });
                })
                .ForMember(dest => dest.source_items, opts =>
                {
                    opts.Condition(src => src.SourceItems.Any());
                    opts.ResolveUsing<ICollection<string>>((src, dest) =>
                    {
                        return src.SourceItems
                            .Select(q => q.Title)
                            .ToList();
                    });
                });

            base.CreateMap<SourceDto, Source>()
                //.SkipNavigationProperties()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
                .ForMember(dest => dest.Tags, opts =>
                {
                    opts.Condition(src => !string.IsNullOrEmpty(src.tags));
                    opts.ResolveUsing<ValueResolver>();
                })
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.title))
                .ForMember(dest => dest.SourceItems, opts =>
                {
                    opts.Condition(src => src.source_items.Any());
                    opts.ResolveUsing<MemberValueResolver, ICollection<string>>(src => src.source_items);
                });
        }
    }
}
