using System;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// Caching
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class IgnoreResultFilterAttribute : Attribute
    {
        /// <summary>
        /// IgnoreResultFilterAttribute
        /// </summary>
        public IgnoreResultFilterAttribute()
        {
        }

    }
}
