using AspectCore.Extensions.DependencyInjection;
using AspectCore.Injector;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Caching;
using SnowLeopard.Caching.Abstractions;

namespace SnowLeopard
{
    /// <summary>
    /// RedisCacheExtensions
    /// </summary>
    public static class MemoryCachingExtensions
    {
        /// <summary>
        /// AddSnowLeopardMemoryCache
        /// </summary>
        /// <param name="services"></param>
        public static IServiceResolver AddSnowLeopardMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddTransient<ICachingProvider, MemoryCachingProvider>();

            //将IServiceCollection的服务添加到ServiceContainer容器中
            var container = services.ToServiceContainer();
            return container.Build();

        }

    }
}
