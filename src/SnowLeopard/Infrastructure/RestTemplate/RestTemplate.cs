using Consul;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// 会自动到Consul中解析服务的Rest客户端，能把"http://ProductService/api/Product/"这样的虚拟地址
    /// 按照客户端负载均衡算法解析为http://192.168.1.10:8080/api/Product/这样的真实地址
    /// </summary>
    public class RestTemplate
    {
        public string ConsulServerUrl { get; set; } = "http://127.0.0.1:8500";
        private HttpClient _httpClient;

        /// <summary>
        /// RestTemplate
        /// </summary>
        /// <param name="httpClient"></param>
        public RestTemplate(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// RestTemplate
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="consulServerUrl"></param>
        public RestTemplate(HttpClient httpClient, string consulServerUrl)
        {
            ConsulServerUrl = consulServerUrl;
            _httpClient = httpClient;
        }

        /// <summary>
        /// 获取服务的第一个实现地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private async Task<string> ResolveRootUrlAsync(string serviceName)
        {
            using (var consulClient = new ConsulClient(c => c.Address = new Uri(ConsulServerUrl)))
            {
                var services = (await consulClient.Agent.Services()).Response.Values
                    .Where(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
                if (!services.Any())
                {
                    throw new ArgumentException($"找不到服务{serviceName }的任何实例");
                }
                else
                {
                    //根据当前时钟毫秒数对可用服务个数取模，取出一台机器使用
                    var service = services.ElementAt(Environment.TickCount % services.Count());
                    return $"{service.Address}:{service.Port}";
                }
            }
        }

        /// <summary>
        /// 把http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> ResolveUrlAsync(string url)
        {
            var uri = new Uri(url);
            string serviceName = uri.Host;//apiservice1
            string realRootUrl = await ResolveRootUrlAsync(serviceName);
            return $"{uri.Scheme}://{realRootUrl}{uri.PathAndQuery}";
        }

        /// <summary>
        /// 发出Get请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> GetAsync<T>(string url, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Get;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                RestResponse<T> respEntity = await SendForEntityAsync<T>(requestMsg);
                return respEntity;
            }
        }

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> PostAsync<T>(string url, object body = null, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Post;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                RestResponse<T> respEntity = await SendForEntityAsync<T>(requestMsg);
                return respEntity;
            }
        }

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">body</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse> PostAsync(string url, object body = null, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Post;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var resp = await SendAsync(requestMsg);
                return resp;
            }
        }

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> PutAsync<T>(string url, object body = null, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Put;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                RestResponse<T> respEntity = await SendForEntityAsync<T>(requestMsg);
                return respEntity;
            }
        }

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse> PutAsync(string url, object body = null, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Put;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var resp = await SendAsync(requestMsg);
                return resp;
            }
        }

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> DeleteAsync<T>(string url, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Delete;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                RestResponse<T> respEntity = await SendForEntityAsync<T>(requestMsg);
                return respEntity;
            }
        }

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <returns></returns>
        public async Task<RestResponse> DeleteAsync(string url, HttpRequestHeaders requestHeaders = null)
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage())
            {
                if (requestHeaders != null)
                {
                    foreach (var header in requestHeaders)
                    {
                        requestMsg.Headers.Add(header.Key, header.Value);
                    }
                }
                requestMsg.Method = System.Net.Http.HttpMethod.Delete;
                //http://apiservice1/api/values转换为http://192.168.1.1:5000/api/values
                requestMsg.RequestUri = new Uri(await ResolveUrlAsync(url));
                var resp = await SendAsync(requestMsg);
                return resp;
            }
        }

        /// <summary>
        /// 发出Http请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="requestMsg">请求数据</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendForEntityAsync<T>(HttpRequestMessage requestMsg)
        {
            var result = await _httpClient.SendAsync(requestMsg);
            var respEntity = new RestResponse<T>
            {
                StatusCode = (int)result.StatusCode
            };
            respEntity.Headers = respEntity.Headers;
            string bodyStr = await result.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(bodyStr))
            {
                respEntity.Body = JsonConvert.DeserializeObject<T>(bodyStr);
            }

            return respEntity;
        }

        /// <summary>
        /// 发出Http请求
        /// </summary>
        /// <param name="requestMsg">请求数据</param>
        /// <returns></returns>
        public async Task<RestResponse> SendAsync(HttpRequestMessage requestMsg)
        {
            var result = await _httpClient.SendAsync(requestMsg);
            var response = new RestResponse
            {
                StatusCode = (int)result.StatusCode,
                Headers = result.Headers
            };
            return response;
        }
    }

}
