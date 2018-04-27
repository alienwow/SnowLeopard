using Lynx.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// UnixTimestampConvert
    /// </summary>
    public class UnixTimestampConvert : DateTimeConverterBase
    {
        /// <summary>
        /// ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullable = objectType.IsNullable();
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                {
                    throw new Exception(string.Format("不能转换null value to {0}.", objectType));
                }

                return null;
            }

            try
            {
                //将时间统一处理成本地时间
                if (reader.TokenType == JsonToken.Integer)// 时间戳转时间
                {
                    return Convert.ToInt64(reader.Value).ToUtcTime();
                }
                else if (reader.TokenType == JsonToken.Date)
                {
                    if (reader.Value.GetType() == typeof(DateTime))
                    {
                        return ((DateTime)reader.Value).ToLocalTime();
                    }
                    else
                    {
                        return reader.Value;
                    }
                }
                else if (reader.TokenType == JsonToken.String)// 字符串转时间
                {
                    return DateTime.Parse(reader.Value as string);
                }
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType));
            }
            throw new Exception(string.Format("Unexpected token {0} when parsing enum", reader.TokenType));
        }

        /// <summary>
        /// WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (value is DateTime dateTime)
            {
                writer.WriteValue(dateTime.ToUnixTimestamp());
            }
            else
            {
                throw new JsonSerializationException("Unexpected value when converting date. ");
            }
        }
    }

}
