using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SnowLeopard.Infrastructure.Json
{
    /// <summary>
    /// JsonSerializerSetting
    /// </summary>
    public class JsonSerializerSetting
    {
        /// <summary>
        /// InitJsonSerializerSetting
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        public static void InitJsonSerializerSetting(JsonSerializerSettings jsonSerializerSettings)
        {
            DefaultJsonSerializerSettings = jsonSerializerSettings ?? throw new ArgumentNullException();

            //忽略循环引用
            DefaultJsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //将时间转换为时间戳
            DefaultJsonSerializerSettings.ContractResolver = new DateTimeContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        }

        /// <summary>
        /// DefaultJsonSerializerSettings
        /// </summary>
        public static JsonSerializerSettings DefaultJsonSerializerSettings { get; private set; }
    }
}
