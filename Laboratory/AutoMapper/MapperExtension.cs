using AutoMapper;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Laboratory.AutoMapper
{
    public static class MapperExtension
    {
        /// <summary>
        /// 跳过导航属性
        /// </summary>
        /// <param name="destinationMember"></param>
        public static IMappingExpression<TSource, TDestination> SkipNavigationProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> destinationMember)
        {
            var destNavigationProperties = typeof(TDestination).GetNavigationProperties();
            foreach (var destNavigationProperty in destNavigationProperties)
            {
                destinationMember.ForMember(destNavigationProperty.Name, opts => opts.Ignore());
            }

            return destinationMember;
        }

        /// <summary>
        /// 跳过导航属性/overload
        /// </summary>
        /// <typeparam name="TSource">source</typeparam>
        /// <typeparam name="TKey">destination.properties</typeparam>
        /// <typeparam name="TDestination">destination</typeparam>
        /// <param name="destinationMember">destinationMember</param>
        /// <param name="expression">忽略的destination.properties</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> SkipNavigationProperties<TSource, TKey, TDestination>(this IMappingExpression<TSource, TDestination> destinationMember, params Expression<Func<TDestination, TKey>>[] expression)
        {
            //获取destination的所有导航属性
            var destNavigationProperties = typeof(TDestination).GetNavigationProperties();
            //跳过的导航属性
            var skipNavigationProperties = expression.LambdaProperties();

            expression.LambdaProperties();

            foreach (var destNavigationProperty in destNavigationProperties)
            {
                if (skipNavigationProperties.Contains(destNavigationProperty.Name)) continue;
                destinationMember.ForMember(destNavigationProperty.Name, opts => opts.Ignore());
            }

            return destinationMember;
        }

        /// <summary>
        /// 映射指定属性的值，除此之外全部忽略
        /// </summary>
        /// <typeparam name="TSource">source</typeparam>
        /// <typeparam name="TKey">key</typeparam>
        /// <typeparam name="TDestination">dto</typeparam>
        /// <param name="destinationMember">member(dto)</param>
        /// <param name="expression">keys</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> SetWithinProperties<TSource, TKey, TDestination>(this IMappingExpression<TSource, TDestination> destinationMember, params Expression<Func<TSource, TKey>>[] expression)
        {
            var destProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Where(w => w.CanRead && w.CanWrite);

            var withinFields = expression.LambdaProperties();

            foreach (var destProperty in destProperties)
            {
                if (!withinFields.Contains(destProperty.Name)) destinationMember.ForMember(destProperty.Name, opts => opts.Ignore());
            }

            return destinationMember;
        }

        /// <summary>
        /// 设定除指定字段外的其他所有属性的值
        /// </summary>
        /// <typeparam name="TSource">source</typeparam>
        /// <typeparam name="TKey">key</typeparam>
        /// <typeparam name="TDestination">dto</typeparam>
        /// <param name="destinationMember">member(dto)</param>
        /// <param name="expression">keys</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> SetWithoutProperties<TSource, TKey, TDestination>(this IMappingExpression<TSource, TDestination> destinationMember, params Expression<Func<TSource, TKey>>[] expression)
        {
            var withoutFields = expression.LambdaProperties();

            var destProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Where(w => w.CanRead && w.CanWrite && withoutFields.Contains(w.Name));

            foreach (var destProperty in destProperties)
            {
                destinationMember.ForMember(destProperty.Name, opts => opts.Ignore());
            }

            return destinationMember;
        }

    }
}