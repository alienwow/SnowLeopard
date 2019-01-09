using Microsoft.Extensions.DependencyInjection;

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
            services.AddSingleton<ICachingKeyGenerater, DefaultCachingKeyGenerater>();
            return services;
        }

    }
}
