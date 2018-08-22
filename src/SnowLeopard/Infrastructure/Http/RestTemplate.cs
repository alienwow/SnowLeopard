using Newtonsoft.Json;
using SnowLeopard.Infrastructure.Consul;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Http
{
    public class RestTemplate : SnowLeopardHttpClient
    {
        private readonly ServiceDiscovery _serviceDiscovery;

        public RestTemplate(
            IHttpClientFactory httpClientFactory,
            ServiceDiscovery serviceDiscovery
        ) : base(httpClientFactory)
        {
            _serviceDiscovery = serviceDiscovery;
        }

        #region Get

        /// <summary>
        /// 发出Get请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public override async Task<HttpResponse<T>> GetAsync<T>(
            string url,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.GetAsync<T>(url, requestHeaders, contentType, settings);
        }

        /// <summary>
        /// 发出Get请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public override async Task<HttpResponse<string>> GetAsync(
            string url,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.GetAsync(url, requestHeaders, contentType);
        }

        #endregion

        #region Post

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public override async Task<HttpResponse<T>> PostAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.PostAsync<T>(url, body, requestHeaders, contentType, settings);
        }

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public override async Task<HttpResponse<string>> PostAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.PostAsync(url, body, requestHeaders, contentType);
        }

        #endregion

        #region Put

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public override async Task<HttpResponse<T>> PutAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.PutAsync<T>(url, body, requestHeaders, contentType, settings);
        }

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public override async Task<HttpResponse<string>> PutAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.PutAsync(url, body, requestHeaders, contentType);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public override async Task<HttpResponse<T>> DeleteAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.DeleteAsync<T>(url, body, requestHeaders, contentType, settings);
        }

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public override async Task<HttpResponse<string>> DeleteAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            url = await _serviceDiscovery.ResolveUrlAsync(url);
            return await base.DeleteAsync(url, body, requestHeaders, contentType, settings);
        }

        #endregion
    }
}
