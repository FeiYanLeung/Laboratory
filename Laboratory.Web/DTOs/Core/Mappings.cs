using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Laboratory.Web.Dto
{
    public static class Mappings
    {
        public static void Register()
        {
            #region 使用profile方式注册
            {
                var profileType = typeof(Profile);      //使用基类限制
                var assemblies = Assembly.GetExecutingAssembly()
                    .GetExportedTypes()
                    .Where(w => w.BaseType != null && profileType.Equals(w.BaseType))
                    .ToList();

                Mapper.Initialize(cfg =>
                {
                    #region 基础类型转换

                    //string to int
                    cfg.CreateMap<string, int>().ConvertUsing((s) => int.Parse(s));
                    //double to decimal
                    cfg.CreateMap<double, decimal>().ConvertUsing((s) => Convert.ToDecimal(s));
                    //decimal to double
                    cfg.CreateMap<decimal, double>().ConvertUsing((s) => Convert.ToDouble(s));

                    #endregion

                    #region 自定义类型转换

                    cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());

                    #endregion

                    assemblies.ForEach(e =>
                    {
                        cfg.AddProfile(e);
                    });
                });
            }
            #endregion

            //判断Destination类中的所有属性是否都被映射，如果存在未被映射的属性，则抛出异常。
            Mapper.AssertConfigurationIsValid();
        }
    }
}
