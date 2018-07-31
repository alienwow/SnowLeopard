using Newtonsoft.Json;
using SnowLeopard.Infrastructure.Consul;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// RestTemplate
    /// </summary>
    public class RestTemplate
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceDiscovery _serviceDiscovery;

        /// <summary>
        /// RestTemplate
        /// </summary>
        /// <param name="httpClient"></param>
        public RestTemplate(HttpClient httpClient)
        {
            _serviceDiscovery = GlobalServices.GetRequiredService<ServiceDiscovery>();
            _httpClient = httpClient;
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                requestMsg.RequestUri = new Uri(await _serviceDiscovery.ResolveUrlAsync(url));
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
                respEntity.Body = JsonConvert.DeserializeObject<T>(bodyStr, APIHelper.DefaultJsonSerializerSettings);
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
