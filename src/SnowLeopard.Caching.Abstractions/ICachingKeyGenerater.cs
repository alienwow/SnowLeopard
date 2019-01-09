using System.Collections.Generic;

namespace SnowLeopard.Caching.Abstractions
{
    public interface ICachingKeyGenerater
    {
        /// <summary>
        /// 生成 CachingKey
        /// </summary>
        /// <param name="cachingKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GenerateCachingKey(string cachingKey, IList<string> parameters);

        /// <summary>
        /// FormatArguments
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IList<string> FormatArguments(object[] arguments);

    }
}
