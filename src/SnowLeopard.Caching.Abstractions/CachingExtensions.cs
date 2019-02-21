using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SnowLeopard.Caching.Abstractions
{
    public static class CachingExtensions
    {
        /// <summary>
        /// AddSnowLeopardRedisCache
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCachingCore(this IServiceCollection services)
        {
            services.TryAddSingleton<ICachingKeyGenerater, DefaultCachingKeyGenerater>();
            return services;
        }

    }
}
