using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SnowLeopard.Infrastructure.Consul;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// ServiceDiscovery Extension
    /// </summary>
    public static class ServiceDiscoveryExtension
    {
        /// <summary>
        /// AddSnowLeopardServiceDiscovery
        /// </summary>
        /// <param name="services"></param>
        public static void AddSnowLeopardServiceDiscovery(this IServiceCollection services)
        {
            services.TryAddSingleton<ServiceDiscovery, DefaultServiceDiscovery>();
        }
    }
}
