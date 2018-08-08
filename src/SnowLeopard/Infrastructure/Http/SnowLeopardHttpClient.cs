using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Http
{
    public class SnowLeopardHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public SnowLeopardHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Get

        /// <summary>
        /// 发出Get请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<T>> GetAsync<T>(
            string url,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);

                return await SendAsync<T>(requestMsg, settings);
            }
        }

        /// <summary>
        /// 发出Get请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<string>> GetAsync(
            string url,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);

                return await SendForStringAsync(requestMsg);
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<T>> PostAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendAsync<T>(requestMsg);
            }
        }

        /// <summary>
        /// 发出Post请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<string>> PostAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Post, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendForStringAsync(requestMsg);
            }
        }

        #endregion

        #region Put

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<T>> PutAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Put, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendAsync<T>(requestMsg);
            }
        }

        /// <summary>
        /// 发出Put请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<string>> PutAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json"
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Put, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendForStringAsync(requestMsg);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<T>> DeleteAsync<T>(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Delete, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendAsync<T>(requestMsg);
            }
        }

        /// <summary>
        /// 发出Delete请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="body">请求数据，将会被json序列化后放到请求报文体中</param>
        /// <param name="requestHeaders">请求额外的报文头信息</param>
        /// <param name="contentType">default to application/json</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public virtual async Task<HttpResponse<string>> DeleteAsync(
            string url,
            object body = null,
            HttpRequestHeaders requestHeaders = null,
            string contentType = "application/json",
            JsonSerializerSettings settings = null
        )
        {
            using (HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Delete, new Uri(url)))
            {
                requestMsg.ProcessHttpRequestHeaders(requestHeaders);
                requestMsg.Content = new StringContent(JsonConvert.SerializeObject(body));
                requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return await SendForStringAsync(requestMsg);
            }
        }

        #endregion

        /// <summary>
        /// 发出Http请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="requestMsg">请求数据</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns></returns>
        public async Task<HttpResponse<T>> SendAsync<T>(
            HttpRequestMessage requestMsg,
            JsonSerializerSettings settings = null
        )
        {
            var result = await SendAsync(requestMsg);
            return new HttpResponse<T>(result, settings);
        }

        /// <summary>
        /// 发出Http请求
        /// </summary>
        /// <typeparam name="T">响应报文反序列类型</typeparam>
        /// <param name="requestMsg">请求数据</param>
        /// <returns></returns>
        public async Task<HttpResponse<string>> SendForStringAsync(HttpRequestMessage requestMsg)
        {
            var result = await SendAsync(requestMsg);

            var respEntity = new HttpResponse<string>
            {
                StatusCode = (int)result.StatusCode
            };
            respEntity.Headers = respEntity.Headers;
            respEntity.Body = await result.Content.ReadAsStringAsync();

            return respEntity;
        }

        /// <summary>
        /// 发出Http请求
        /// </summary>
        /// <param name="requestMsg">请求数据</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMsg)
        {
            var _httpClient = _httpClientFactory.CreateClient();
            return await _httpClient.SendAsync(requestMsg);
        }

    }

    /// <summary>
    /// HttpExtension
    /// </summary>
    internal static class HttpExtension
    {
        public static void ProcessHttpRequestHeaders(this HttpRequestMessage requestMsg, HttpRequestHeaders requestHeaders)
        {
            if (requestHeaders != null)
            {
                foreach (var header in requestHeaders)
                {
                    requestMsg.Headers.Add(header.Key, header.Value);
                }
            }
        }
    }
}
