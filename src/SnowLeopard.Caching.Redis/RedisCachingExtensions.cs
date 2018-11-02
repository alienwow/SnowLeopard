using System;
using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Caching;

namespace SnowLeopard
{
    /// <summary>
    /// RedisCachingExtensions
    /// </summary>
    public static class RedisCachingExtensions
    {
        /// <summary>
        /// AddSnowLeopardRedisCache
        /// </summary>
        /// <param name="services"></param>
        public static IServiceProvider AddSnowLeopardRedisCache(this IServiceCollection services)
        {
            services.AddTransient<ICachingProvider, RedisCachingProvider>();

            //将IServiceCollection的服务添加到ServiceContainer容器中
            var container = services.ToServiceContainer();
            return container.Build();

            //services.BuildAspectInjectorProvider();
            //services.WeaveDynamicProxyService();
        }

    }
}
