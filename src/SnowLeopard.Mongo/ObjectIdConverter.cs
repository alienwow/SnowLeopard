using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using SnowLeopard.Lynx.Extensions;

namespace SnowLeopard.Infrastructure.Json
{
    /// <summary>
    /// ObjectIdConverter
    /// </summary>
    public class ObjectIdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (value is ObjectId objectId)
            {
                writer.WriteValue(objectId.ToString());
            }
            else
            {
                throw new JsonSerializationException("Unexpected value when converting ObjectId. ");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullable = objectType.IsNullable();

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                {
                    throw new Exception(string.Format("不能转换null value to {0}.", objectType));
                }

                return null;
            }

            if (reader.TokenType == JsonToken.String)
            {
                var value = reader.Value.ToString();
                if (string.IsNullOrEmpty(value))
                    return ObjectId.Empty;

                if (ObjectId.TryParse(reader.Value.ToString(), out ObjectId result))
                {
                    return result;
                }
                else
                {
                    throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType));
                }
            }
            else
            {
                throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType));
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ObjectId);
        }
    }
}
