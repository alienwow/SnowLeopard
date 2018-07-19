using System.Net.Http.Headers;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// Rest响应结果
    /// </summary>
    public class RestResponse
    {
        /// <summary>
        /// 响应状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 响应的报文头
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }
    }

    /// <summary>
    /// 带响应报文的Rest响应结果，而且json报文会被自动反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestResponse<T> : RestResponse
    {
        /// <summary>
        /// 响应报文体json反序列化的内容
        /// </summary>
        public T Body { get; set; }
    }
}
