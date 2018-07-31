using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //将时间转换为时间戳
                    options.SerializerSettings.ContractResolver = new DateTimeContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()// 使用驼峰样式
                    };
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
