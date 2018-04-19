using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Laboratory.AutoMapper
{
    public static class Mappings
    {
        public static void Register()
        {
            //使用profile注册方式
            var profileRegister = true;

            if (profileRegister)
            {
                #region 使用profile方式注册
                {
                    var iProfileType = typeof(IProfile);    //使用接口限定
                    var profileType = typeof(Profile);      //使用基类限制
                    var assemblies = Assembly.GetExecutingAssembly()
                        .GetExportedTypes()
                        //.Where(w => w.GetInterfaces().Contains(iProfileType))
                        .Where(w => w.BaseType != null && profileType.Equals(w.BaseType))
                        .ToList();

                    Mapper.Initialize(cfg =>
                    {
                        //基础类型转换
                        cfg.CreateMap<string, int>().ConvertUsing((s, i) => { return int.Parse(s); });
                        //自定义类型转换
                        cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());

                        assemblies.ForEach(e =>
                        {
                            cfg.AddProfile(e);
                        });
                    });
                }
                #endregion
            }
            else
            {
                #region 使用统一方式注册
                /*
                {
                    Mapper.Initialize(cfg =>
                    {
                        //基础类型转换
                        cfg.CreateMap<string, int>().ConvertUsing((s, i) => { return int.Parse(s); });
                        //自定义类型转换
                        cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());

                        //当字段名称、数据类型不一致时，可以使用ForMember映射字段
                        //当字段名称、数据格式一致时，可以使用cfg.CreateMap<Source, Destination>();而不需要使用ForMember注册
                        //自定义实体值的转换，适用于目标与源实体直接的值类型计算
                        cfg.CreateMap<Source, SourceDto>()
                            .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.Id))
                            .ForMember(dest => dest.tags, opts => opts.ResolveUsing<MemberValueResolver, ICollection<int>>(src => src.Tags))
                            .ForSourceMember(src => src.Title, opts => opts.Ignore())
                            .ForMember(dest => dest.title, opts => opts.MapFrom(src => src.Title))
                            .ReverseMap();  //如果不进行反向映射，则dest无法将值赋给source
                    });
                }
                */
                #endregion
            }

            //判断Destination类中的所有属性是否都被映射，如果存在未被映射的属性，则抛出异常。
            Mapper.AssertConfigurationIsValid();
        }
    }
}
