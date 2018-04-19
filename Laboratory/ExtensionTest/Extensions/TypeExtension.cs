using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Laboratory
{
    public static class TypeExtension
    {
        /// <summary>
        ///  获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDescription(this Type type, bool inherit = false)
        {
            DescriptionAttribute desc = type.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? null : desc.Description;
        }

        /// <summary>
        ///  获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string ToDescription(this MemberInfo member, bool inherit = false)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? null : desc.Description;
        }

        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Any(m => (m as T) != null);
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).SingleOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        /// <summary>
        /// 获取MethodInfo
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfo(this PropertyInfo propertyInfo)
        {
            return propertyInfo.CanRead ? propertyInfo.GetGetMethod(true) : propertyInfo.GetSetMethod(true);
        }

        /// <summary>
        /// 判断是否是override的字段
        /// </summary>
        /// <param name="propertyInfo">类型类型成员</param>
        /// <returns></returns>
        public static bool IsOverride(this PropertyInfo propertyInfo)
        {
            var methodInfo = propertyInfo.GetMethodInfo();
            return methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType;
        }

        /// <summary>
        /// 获取实体类的导航属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetNavigationProperties(this Type type)
        {
            var collectionType = typeof(ICollection<>);
            var extendPrimitiveTypes = new List<Type>()
            {
                typeof(string)
            };
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            foreach (var property in properties)
            {
                if (!property.CanRead) continue;
                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GenericTypeArguments.Any(t => !t.IsPrimitive) && property.PropertyType.GetGenericTypeDefinition() == collectionType)
                    {
                        yield return property;
                    }
                }
                else if (property.GetMethodInfo().IsVirtual && !property.PropertyType.IsPrimitive && !extendPrimitiveTypes.Contains(property.PropertyType))
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// 获取实体类更新的字段
        /// </summary>
        /// <param name="type">实体类的Type</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetUpdateableProperties(this Type type)
        {
            var collectionType = typeof(ICollection<>);
            var properties = type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            foreach (var property in properties)
            {
                if (!property.CanRead) continue;
                if (property.PropertyType.IsGenericType && property.PropertyType.GenericTypeArguments.Any(t => t.IsPrimitive) && property.PropertyType.GetGenericTypeDefinition() == collectionType)
                {
                    continue;
                }
                yield return property;
            }
        }

        /// <summary>
        /// 获取lambda中的字段信息
        /// </summary>
        /// <typeparam name="T">源对象</typeparam>
        /// <typeparam name="TKey">源对象的属性</typeparam>
        /// <param name="expressions">lambda表达式</param>
        /// <returns>lambda中指示的属性</returns>
        public static List<string> LambdaProperties<T, TKey>(this IEnumerable<Expression<Func<T, TKey>>> expressions)
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
