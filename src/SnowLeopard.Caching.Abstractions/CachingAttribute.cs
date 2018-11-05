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
        /// <param name="expiration">缓存过期时间 默认30s</param>
        public CachingAttribute(int expiration = 30)
        {
            Expiration = expiration;
        }

        /// <summary>
        /// Caching Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Expiration
        /// </summary>
        public int Expiration { get; set; }

    }
}
