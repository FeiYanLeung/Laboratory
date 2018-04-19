using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Laboratory.Web.Dto
{
    public static class MapperExtension
    {
        /// <summary>
        /// 跳过导航属性
        /// </summary>
        /// <param name="destinationMember"></param>
        public static IMappingExpression<TSource, TDestination> SkipVirtualProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> destinationMember)
        {
            var destVirtualProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(w => (w.CanRead ? w.GetGetMethod(true) : w.GetSetMethod(true)).IsVirtual);

            foreach (var destVirtualProperty in destVirtualProperties)
            {
                destinationMember.ForMember(destVirtualProperty.Name, opts => opts.Ignore());
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
            return destinationMember;

            var destProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(w => w.CanRead && w.CanWrite);

            var withinFields = lambdaFields(expression);

            destinationMember.ForAllMembers(opts => opts.Ignore());

            foreach (var destProperty in destProperties)
            {
                destinationMember.ForMember(destProperty.Name, opts => opts.MapFrom(destProperty.Name));
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
            return destinationMember;

            var withoutFields = lambdaFields(expression);

            var destProperties = typeof(TDestination)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(w => w.CanRead && w.CanWrite && withoutFields.Contains(w.Name));

            foreach (var destProperty in destProperties)
            {
                destinationMember.ForMember(destProperty.Name, opts => opts.Ignore());
            }

            return destinationMember;
        }

        /// <summary>
        /// 获取Lambda中的字段名称
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="expressions"></param>
        /// <returns></returns>
        private static List<string> lambdaFields<TSource, TKey>(params Expression<Func<TSource, TKey>>[] expressions)
        {
            var properties = new List<string>();

            foreach (var expression in expressions)
            {
                var exprBody = expression.Body;
                if (exprBody is UnaryExpression)
                {
                    properties.Add(((MemberExpression)((UnaryExpression)exprBody).Operand).Member.Name);
                }
                else if (exprBody is MemberExpression)
                {
                    properties.Add(((MemberExpression)exprBody).Member.Name);
                }
                else if (exprBody is ParameterExpression)
                {
                    properties.Add(((ParameterExpression)exprBody).Type.Name);
                }
            }
            return properties;
        }
    }
}