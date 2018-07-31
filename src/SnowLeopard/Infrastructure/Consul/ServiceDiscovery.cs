using Consul;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Consul
{
    /// <summary>
    /// ServiceDiscovery
    /// </summary>
    public abstract class ServiceDiscovery
    {
        protected readonly string _consulServerUrl = "http://127.0.0.1:8500";

        /// <summary>
        /// ServiceDiscovery
        /// </summary>
        public ServiceDiscovery() { }

        /// <summary>
        /// ServiceDiscovery
        /// </summary>
        /// <param name="consulServerUrl"></param>
        public ServiceDiscovery(string consulServerUrl)
        {
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
