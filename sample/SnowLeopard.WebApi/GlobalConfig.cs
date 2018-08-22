using SnowLeopard.Mongo;

namespace SnowLeopard.WebApi
{
    /// <summary>
    /// GlobalConfig
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 站点监听地址
        /// </summary>
        public string ApplicationUrl { get; set; }

        /// <summary>
        /// MongoOption
        /// </summary>
        public MongoOption MongoOption { get; set; }
    }
}
