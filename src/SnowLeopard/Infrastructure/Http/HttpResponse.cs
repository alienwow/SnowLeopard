using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SnowLeopard.Infrastructure.Http
{
    /// <summary>
    /// HttpResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponse<T>
    {
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        /// <summary>
        /// HttpResponse
        /// </summary>
        public HttpResponse()
        {

        }

        /// <summary>
        /// HttpResponse
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="settings">JsonSerializerSettings</param>
        public HttpResponse(HttpResponseMessage httpResponseMessage, JsonSerializerSettings settings = null)
        {
            StatusCode = (int)httpResponseMessage.StatusCode;
            Headers = httpResponseMessage.Headers;
            string bodyStr = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrWhiteSpace(bodyStr))
            {
                Body = JsonConvert.DeserializeObject<T>(bodyStr, settings ?? APIHelper.DefaultJsonSerializerSettings);
            }
        }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 响应的报文头
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// 响应报文体json反序列化的内容
        /// </summary>
        public T Body { get; set; }
    }
}
