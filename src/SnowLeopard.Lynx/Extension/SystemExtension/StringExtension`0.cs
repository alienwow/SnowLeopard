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
        /// <summary>
        /// 将数据列表转化为逗号分隔的字符串
        /// </summary>
        /// <typeparam name="T">源数据类</typeparam>
        /// <typeparam name="V">文本字段</typeparam>
        /// <param name="self">源数据List</param>
        /// <param name="textField">要拼接的文本字段</param>
        /// <param name="Format">转化文本字段的表达式</param>
        /// <param name="seperator">分隔符，默认为逗号</param>
        /// <returns>转化后的字符串</returns>
        public static string ToSpratedString<T, V>(this IEnumerable<T> self, Expression<Func<T, V>> textField, Func<V, string> Format = null, string seperator = ",")
        {
            if (self == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            //循环所有数据
            for (int i = 0; i < self.Count(); i++)
            {
                //获取文本字段的值
                V text = textField.Compile().Invoke(self.ElementAt(i));
                var str = string.Empty;
                //如果有转换函数，则调用获取转换后的字符串
                if (Format == null && text != null)
                {
                    str = text.ToString();
                }
                else
                {
                    str = Format.Invoke(text);
                }
                sb.Append(str);
                //拼接分隔符
                if (i < self.Count() - 1)
                {
                    sb.Append(seperator);
                }
            }
            //返回转化后的字符串
            return sb.ToString();
        }

        /// <summary>
        /// ToSpratedString
        /// </summary>
        /// <param name="self"></param>
        /// <param name="Format"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToSpratedString(this IEnumerable self, Func<object, string> Format = null, string seperator = ",")
        {
            string rv = "";
            if (self == null)
            {
                return rv;
            }
            foreach (var item in self)
            {
                if (Format == null)
                {
                    rv += item.ToString() + seperator;
                }
                else
                {
                    rv += Format.Invoke(item) + seperator;
                }
            }
            if (rv.Length > 0)
            {
                rv = rv.Substring(0, rv.Length - 1);
            }
            return rv;
        }

        /// <summary>
        /// ToSpratedString
        /// </summary>
        /// <param name="self"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToSpratedString(this NameValueCollection self, string seperator = ",")
        {
            string rv = "";
            if (self == null)
            {
                return rv;
            }
            foreach (var item in self)
            {
                rv += item.ToString() + "=" + self[item.ToString()] + seperator;
            }
            if (rv.Length > 0)
            {
                rv = rv.Substring(0, rv.Length - 1);
            }
            return rv;
        }

        [DebuggerStepThrough]
        public static string ToSpaceSeparatedString(this IEnumerable<string> list)
        {
            if (list == null)
            {
                return "";
            }

            var sb = new StringBuilder(100);

            foreach (var element in list)
            {
                sb.Append(element + " ");
            }

            return sb.ToString().Trim();
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> FromSpaceSeparatedString(this string input)
        {
            input = input.Trim();
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// NullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static bool IsMissingOrTooLong(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (value.Length > maxLength)
            {
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 以 / 开始
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string EnsureLeadingSlash(this string url)
        {
            if (!url.StartsWith("/"))
            {
                return "/" + url;
            }

            return url;
        }

        /// <summary>
        /// 以 / 结尾
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string EnsureTrailingSlash(this string url)
        {
            if (!url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        /// <summary>
        /// 移除开头的 /
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string RemoveLeadingSlash(this string url)
        {
            if (url != null && url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url;
        }

        /// <summary>
        /// 移除结尾的 /
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string RemoveTrailingSlash(this string url)
        {
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string CleanUrlPath(this string url)
        {
            if (string.IsNullOrWhiteSpace(url)) url = "/";

            if (url != "/" && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static bool IsLocalUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            // Allows "/" or "/foo" but not "//" or "/\".
            if (url[0] == '/')
            {
                // url is exactly "/"
                if (url.Length == 1)
                {
                    return true;
                }

                // url doesn't start with "//" or "/\"
                if (url[1] != '/' && url[1] != '\\')
                {
                    return true;
                }

                return false;
            }

            // Allows "~/" or "~/foo" but not "~//" or "~/\".
            if (url[0] == '~' && url.Length > 1 && url[1] == '/')
            {
                // url is exactly "~/"
                if (url.Length == 2)
                {
                    return true;
                }

                // url doesn't start with "~//" or "~/\"
                if (url[2] != '/' && url[2] != '\\')
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string name, string value)
        {
            return url.AddQueryString(name + "=" + UrlEncoder.Default.Encode(value));
        }

        [DebuggerStepThrough]
        public static string AddHashFragment(this string url, string query)
        {
            if (!url.Contains("#"))
            {
                url += "#";
            }

            return url + query;
        }

        public static string GetOrigin(this string url)
        {
            if (url != null)
            {
                var uri = new Uri(url);
                if (uri.Scheme == "http" || uri.Scheme == "https")
                {
                    return $"{uri.Scheme}://{uri.Authority}";
                }
            }

            return null;
        }
    }
}
