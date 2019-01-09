using System;
using System.Collections.Generic;
using System.Linq;

namespace SnowLeopard.Caching.Abstractions
{
    public class DefaultCachingKeyGenerater : ICachingKeyGenerater
    {
        /// <summary>
        /// CachingKey 分割符
        /// </summary>
        public const string SPLIT_CHAR = ":";

        /// <summary>
        /// 生成 CachingKey
        /// </summary>
        /// <param name="cachingKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GenerateCachingKey(string cachingKey, IList<string> parameters)
        {
            parameters.Insert(0, cachingKey);

            return string.Join(SPLIT_CHAR, parameters);
        }

        /// <summary>
        /// FormatArguments
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public IList<string> FormatArguments(object[] arguments)
        {
            if (arguments != null && arguments.Length > 0)
                return arguments.Select(GetArgumentValue).ToList();
            else
                return new List<string> { "0" };
        }

        /// <summary>
        /// GetArgumentValue
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is float || arg is double || arg is decimal || arg is string)
                return arg.ToString();

            if (arg is DateTime || arg is DateTime?)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            if (arg is ICachable)
                return ((ICachable)arg).CacheKey;

            return null;
        }

    }
}
