using Consul;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Consul
{
    /// <summary>
    /// ServiceDiscovery
    /// </summary>
    public abstract class ServiceDiscovery
    {
        protected readonly string _consulServerUrl = "http://127.0.0.1:8500";

        private readonly ILogger _logger;

        /// <summary>
        /// ServiceDiscovery
        /// </summary>
        public ServiceDiscovery()
        {
            _logger = GlobalServices.GetRequiredService<ILogger<ServiceDiscovery>>();
        }

        /// <summary>
        /// ServiceDiscovery
        /// </summary>
        /// <param name="consulServerUrl"></param>
        public ServiceDiscovery(string consulServerUrl)
        {
            _logger = GlobalServices.GetRequiredService<ILogger<ServiceDiscovery>>();
            _consulServerUrl = consulServerUrl;
        }

        /// <summary>
        /// ResolveService
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public abstract Task<CatalogService[]> ResolveServiceAsync(string serviceName);

        /// <summary>
        /// 地址转换
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public abstract Task<string> ResolveUrlAsync(string url);

    }
}
