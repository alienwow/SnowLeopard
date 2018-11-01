using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;

namespace SnowLeopard.Lynx.Extension
{
    /// <summary>
    /// 字符串辅助类
    /// </summary>
    public static partial class StringExtension
    {
        #region ToStringArray

        /// <summary>
        /// ToStringArray
        /// </summary>
        /// <param name="array"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this int[] array, string prefix = "", string suffix = "")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = prefix + array[i].ToString() + suffix;

            return result;
        }

        /// <summary>
        /// ToStringArray
        /// </summary>
        /// <param name="array"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this long[] array, string prefix = "", string suffix = "")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = prefix + array[i].ToString() + suffix;

            return result;
        }

        /// <summary>
        /// ToStringArray
        /// </summary>
        /// <param name="array"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this float[] array, string prefix = "", string suffix = "")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = prefix + array[i].ToString() + suffix;

            return result;
        }

        /// <summary>
        /// ToStringArray
        /// </summary>
        /// <param name="array"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this double[] array, string prefix = "", string suffix = "")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = prefix + array[i].ToString() + suffix;

            return result;
        }

        /// <summary>
        /// ToStringArray
        /// </summary>
        /// <param name="array"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this string[] array, string prefix = "", string suffix = "")
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var result = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = prefix + array[i].ToString() + suffix;

            return result;
        }

        #endregion
    }
}
