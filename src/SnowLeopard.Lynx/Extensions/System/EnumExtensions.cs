using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SnowLeopard.Lynx.Extensions
{
    /// <summary>
    /// 枚举扩展函数
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举显示名称
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>枚举显示名称</returns>
        public static string GetEnumDisplayName(this Enum value)
        {
            return GetEnumDisplayName(value.GetType(), value.ToString());
        }

        /// <summary>
        /// 获取枚举显示名称
        /// </summary>
        /// <param name="self">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <returns>枚举显示名称</returns>
        public static string GetEnumDisplayName(this Type self, string value)
        {
            var rv = string.Empty;
            FieldInfo field = null;

            if (self.IsEnum())
            {
                field = self.GetField(value);
            }
            //如果是nullable的枚举
            if (self.IsGeneric(typeof(Nullable<>)) && self.GetGenericArguments()[0].IsEnum())
            {
                field = self.GenericTypeArguments[0].GetField(value);
            }

            if (field != null)
            {
                var attribs = field.GetCustomAttributes(typeof(DisplayAttribute), true).ToArray();
                if (attribs.Length > 0)
                {
                    rv = ((DisplayAttribute)attribs[0]).GetName();
                }
                else
                {
                    rv = value;
                }
            }
            return rv;
        }

        #region 判断是否为枚举

        /// <summary>
        /// 判断是否为枚举
        /// </summary>
        /// <param name="self">Type类</param>
        /// <returns>判断结果</returns>
        public static bool IsEnum(this Type self)
        {
            return self.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 判断是否为枚举或者可空枚举
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsEnumOrNullableEnum(this Type self)
        {
            if (self == null)
            {
                return false;
            }
            if (self.IsEnum)
            {
                return true;
            }
            else
            {
                if (self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Nullable<>) && self.GetGenericArguments()[0].IsEnum)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetEnumValues(this Type self)
        {
            var enumVals = Enum.GetValues(self);
            return null;
        }
    }
}
