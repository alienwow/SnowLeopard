using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SnowLeopard.Lynx.Extensions
{
    /// <summary>
    /// TypeExtensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断是否是泛型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <param name="innerType">泛型类型</param>
        /// <returns>判断结果</returns>
        public static bool IsGeneric(this Type self, Type innerType)
        {
            if (self.GetTypeInfo().IsGenericType && self.GetGenericTypeDefinition() == innerType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为Nullable`T类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsNullable(this Type self)
        {
            return self.IsGeneric(typeof(Nullable<>));
        }

        /// <summary>
        /// 判断是否为List`T类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsList(this Type self)
        {
            return self.IsGeneric(typeof(List<>));
        }

        /// <summary>
        /// 判断是否为值类型
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsPrimitive(this Type self)
        {
            return self.GetTypeInfo().IsPrimitive || self == typeof(decimal);
        }

        /// <summary>
        /// GetTableName
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetTableName(this Type self)
        {
            var name = string.Empty;

            if (self.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == nameof(TableAttribute)) is TableAttribute tableAttr)
            {
                name = tableAttr.Name;
            }
            else
            {
                name = self.Name + "s";
                if (self.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            return name;
        }

        #region 判断是否是Bool

        /// <summary>
        /// IsBool
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsBool(this Type self)
        {
            return self == typeof(bool);
        }

        /// <summary>
        /// 判断是否是 bool or bool?类型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsBoolOrNullableBool(this Type self)
        {
            if (self == null)
            {
                return false;
            }
            if (self == typeof(bool) || self == typeof(bool?))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
