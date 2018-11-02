using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Infrastructure;
using SnowLeopard.Infrastructure.Json;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// Mvc Extension
    /// </summary>
    public static class MvcExtension
    {
        /// <summary>
        /// Add SnowLeopard Mvc
        /// </summary>
        /// <param name="services"></param>
        public static IMvcBuilder AddSnowLeopardMvc(this IServiceCollection services)
        {
            return services
                .AddMvc(options =>
                {
                    options.AddSnowLeopardFilters();
                })
                .AddControllersAsServices()
                .AddJsonOptions(options =>// 全局配置Json序列化处理
                {
                    JsonSerializerSetting.InitJsonSerializerSetting(options.SerializerSettings);
                });
        }

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
