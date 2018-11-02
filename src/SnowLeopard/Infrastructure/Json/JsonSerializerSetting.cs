using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SnowLeopard.Infrastructure.Json
{
    /// <summary>
    /// JsonSerializerSetting
    /// </summary>
    public static class JsonSerializerSetting
    {
        private static List<Action<JsonSerializerSettings>> _jsonSettingActions;

        /// <summary>
        /// InitJsonSerializerSetting
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        public static void InitJsonSerializerSetting(JsonSerializerSettings jsonSerializerSettings)
        {
            DefaultJsonSerializerSettings = jsonSerializerSettings ?? throw new ArgumentNullException();

            // 设置全局的 Json 序列化配置
            JsonConvert.DefaultSettings = () => DefaultJsonSerializerSettings;

            //忽略循环引用
            DefaultJsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            DefaultJsonSerializerSettings.Converters.Add(new StringEnumConverter(true));

            //将时间转换为时间戳
            DefaultJsonSerializerSettings.ContractResolver = new DateTimeContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            foreach (var jsonSettingAction in _jsonSettingActions)
                jsonSettingAction(DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// 设置 Json
        /// </summary>
        /// <param name="jsonSettings"></param>
        public static void Config(Action<JsonSerializerSettings> jsonSettings)
        {
            if (jsonSettings == null)
                throw new ArgumentNullException(nameof(jsonSettings));

            if (_jsonSettingActions == null)
                _jsonSettingActions = new List<Action<JsonSerializerSettings>>();

            _jsonSettingActions.Add(jsonSettings);
        }

        /// <summary>
        /// DefaultJsonSerializerSettings
        /// </summary>
        public static JsonSerializerSettings DefaultJsonSerializerSettings { get; private set; }

    }
}
