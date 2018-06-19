using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Common
{
    public enum HttpMethodEnum { GET, POST, PUT, DELETE }

    /// <summary>
    /// 有关HTTP请求的辅助类
    /// </summary>
    public class APIHelper
    {
        public const string DEFAULT_USER_AGENT = "SnowLeopard/ASPNETCORE2.1(@Vito.Wu)";

        /// <summary>
        /// 调用远程地址
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postdata">Post数据</param>
        /// <param name="timeout">超时秒数</param>
        /// <param name="proxy">代理</param>
        /// <returns>远程方法返回的内容</returns>
        public static async Task<string> CallAPIAsync(string url, HttpMethodEnum method = HttpMethodEnum.GET, IDictionary<string, string> postdata = null, int? timeout = null, string proxy = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            //  新建http请求
            var request = WebRequest.Create(url) as HttpWebRequest;
            // 如果配置了代理，则使用代理
            if (string.IsNullOrEmpty(proxy) == false)
            {
                request.Proxy = new WebProxy(proxy);
            }

            request.Method = method.ToString();
            // 如果是Post请求，则设置表单
            if (method == HttpMethodEnum.POST || method == HttpMethodEnum.PUT)
            {
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                if (postdata == null || postdata.Count == 0)
                {
                    request.Headers[HttpRequestHeader.ContentLength] = "0";
                }
            }
            request.Headers[HttpRequestHeader.UserAgent] = DEFAULT_USER_AGENT;

            // 设置超时
            if (timeout.HasValue)
            {
                request.ContinueTimeout = timeout.Value;
            }
            request.CookieContainer = new CookieContainer();
            // 填充表单数据
            if (!(postdata == null || postdata.Count == 0))
            {
                var buffer = new StringBuilder();
                var i = 0;
                foreach (string key in postdata.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, postdata[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, postdata[key]);
                    }
                    i++;
                }
                var data = Encoding.UTF8.GetBytes(buffer.ToString());
                using (var stream = await request.GetRequestStreamAsync())
                {
                    stream.Write(data, 0, data.Length);
                }
            }


            var res = await request.GetResponseAsync();
            var wsp = (HttpWebResponse)res;
            Stream st = null;
            // 如果远程服务器采用了gzip，增进行相应的解压缩
            if (wsp.Headers[HttpResponseHeader.ContentEncoding]?.ToLower().Contains("gzip") == true)
            {
                st = new GZipStream(st, CompressionMode.Decompress);
            }
            else
            {
                st = wsp.GetResponseStream();
            }
            // 设置编码
            var encode = Encoding.UTF8;
            if (!string.IsNullOrEmpty(wsp.Headers[HttpResponseHeader.ContentEncoding]))
            {
                encode = Encoding.GetEncoding(wsp.Headers[HttpResponseHeader.ContentEncoding]);
            }
            // 读取内容
            var sr = new StreamReader(st, encode);
            var ss = sr.ReadToEnd();
            sr.Dispose();
            wsp.Dispose();
            return ss;
        }

        /// <summary>
        /// 调用远程方法，返回强类型
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postdata">Post数据</param>
        /// <param name="timeout">超时秒数</param>
        /// <param name="proxy">代理</param>
        /// <returns>强类型</returns>
        public static async Task<T> CallAPIAsync<T>(string url, HttpMethodEnum method = HttpMethodEnum.GET, IDictionary<string, string> postdata = null, int? timeout = null, string proxy = null)
        {
            var s = await CallAPIAsync(url, method, postdata, timeout, proxy);
            return JsonDeserialize<T>(s);
        }

        /// <summary>
        /// 调用远程地址
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postdata">Post数据</param>
        /// <param name="timeout">超时秒数</param>
        /// <param name="proxy">代理</param>
        /// <returns>远程方法返回的内容</returns>
        public static string CallAPI(string url, HttpMethodEnum method = HttpMethodEnum.GET, IDictionary<string, string> postdata = null, int? timeout = null, string proxy = null)
        {
            return CallAPIAsync(url, method, postdata, timeout, proxy).Result;
        }

        /// <summary>
        /// 调用远程方法，返回强类型
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postdata">Post数据</param>
        /// <param name="timeout">超时秒数</param>
        /// <param name="proxy">代理</param>
        /// <returns>强类型</returns>
        public static T CallAPI<T>(string url, HttpMethodEnum method = HttpMethodEnum.GET, IDictionary<string, string> postdata = null, int? timeout = null, string proxy = null)
        {
            var s = CallAPI(url, method, postdata, timeout, proxy);
            return JsonDeserialize<T>(s);
        }

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>Json</returns>
        public static string JsonSerialize(object input)
        {
            var s = JsonConvert.SerializeObject(input);
            if (s != null && s.StartsWith("[") == false)
            {
                s = "[" + s + "]";
            }
            return s;
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="input">Json字符串</param>
        /// <returns>强类型</returns>
        public static T JsonDeserialize<T>(string input)
        {
            try
            {
                T rv = JsonConvert.DeserializeObject<T>(input);
                return rv;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
