using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Infrastructure;
using System.Reflection;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// Mvc Extension
    /// </summary>
    public static class MvcExtension
    {
        /// <summary>
        /// Add SnowLeopard Filters
        /// </summary>
        /// <param name="self"></param>
        public static void AddSnowLeopardFilters(this MvcOptions self)
        {
            self.Filters.Add(typeof(GlobalExceptionFilter));
            self.Filters.Add(typeof(ModelStateFilter));
            self.Filters.Add(typeof(ResultFilter));
        }

        /// <summary>
        /// Add SnowLeopard ApplicationPart
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddSnowLeopardApplicationPart(this IMvcBuilder builder)
        {
            Assembly assembly = null;
            return builder.AddApplicationPart(assembly);
        }
    }
}
