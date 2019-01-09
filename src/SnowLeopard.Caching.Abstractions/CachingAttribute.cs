using System;

namespace SnowLeopard.Caching
{
    /// <summary>
    /// Caching
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : Attribute
    {
        /// <summary>
        /// CachingAttribute
        /// </summary>
        public CachingAttribute()
        {
        }

        /// <summary>
        /// Caching Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 缓存过期时间 默认30s
        /// </summary>
        public int Expiration { get; set; } = 30;

    }
}
