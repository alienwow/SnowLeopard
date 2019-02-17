using System;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        public static IServiceProvider AddSnowLeopardMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.TryAddTransient<ICachingProvider, MemoryCachingProvider>();
            services.AddCachingCore();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterDynamicProxy();

            var serviceProvider = new AutofacServiceProvider(builder.Build());
            GlobalServices.SetServiceProvider(serviceProvider);

            return serviceProvider;
        }

    }
}
