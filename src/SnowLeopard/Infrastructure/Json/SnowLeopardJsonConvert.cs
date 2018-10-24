using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Json
{
    /// <summary>
    /// Provides methods for converting between common language runtime types and JSON types.
    /// </summary>
    public static class SnowLeopardJsonConvert
    {
        /// <summary>
        /// Represents JavaScript's boolean value true as a string. This field is read-only.
        /// </summary>
        public const string True = "true";

        /// <summary>
        /// Represents JavaScript's boolean value false as a string. This field is read-only.
        /// </summary>
        public const string False = "false";

        /// <summary>
        /// Represents JavaScript's null as a string. This field is read-only.
        /// </summary>
        public const string Null = "null";

        /// <summary>
        /// Represents JavaScript's undefined as a string. This field is read-only.
        /// </summary>
        public const string Undefined = "undefined";

        /// <summary>
        /// Represents JavaScript's positive infinity as a string. This field is read-only.
        /// </summary>
        public const string PositiveInfinity = "Infinity";

        /// <summary>
        /// Represents JavaScript's negative infinity as a string. This field is read-only.
        /// </summary>
        public const string NegativeInfinity = "-Infinity";

        /// <summary>
        /// Represents JavaScript's NaN as a string. This field is read-only.
        /// </summary>
        public const string NaN = "NaN";

        #region Serialize

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string SerializeObject(object value)
        {
            return SerializeObject(value, null, (JsonSerializerSettings)null);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using formatting.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">Indicates how the output is formatted.</param>
        /// <returns>
        /// A JSON string representation of the object.
        /// </returns>
        public static string SerializeObject(object value, Formatting formatting)
        {
            return SerializeObject(value, formatting, (JsonSerializerSettings)null);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using a collection of <see cref="JsonConverter"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="converters">A collection converters used while serializing.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string SerializeObject(object value, params JsonConverter[] converters)
        {
            JsonSerializerSettings settings = (converters != null && converters.Length > 0)
                ? new JsonSerializerSettings { Converters = converters }
                : null;

            return SerializeObject(value, null, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using formatting and a collection of <see cref="JsonConverter"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">Indicates how the output is formatted.</param>
        /// <param name="converters">A collection converters used while serializing.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string SerializeObject(object value, Formatting formatting, params JsonConverter[] converters)
        {
            JsonSerializerSettings settings = (converters != null && converters.Length > 0)
                ? new JsonSerializerSettings { Converters = converters }
                : null;

            return SerializeObject(value, null, formatting, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object.
        /// If this is null, default serialization settings will be used.</param>
        /// <returns>
        /// A JSON string representation of the object.
        /// </returns>
        public static string SerializeObject(object value, JsonSerializerSettings settings)
        {
            return SerializeObject(value, null, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using a type, formatting and <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object.
        /// If this is null, default serialization settings will be used.</param>
        /// <param name="type">
        /// The type of the value being serialized.
        /// This parameter is used when <see cref="TypeNameHandling"/> is Auto to write out the type name if the type of the value does not match.
        /// Specifing the type is optional.
        /// </param>
        /// <returns>
        /// A JSON string representation of the object.
        /// </returns>
        public static string SerializeObject(object value, Type type, JsonSerializerSettings settings)
        {
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);

            return SerializeObjectInternal(value, type, jsonSerializer);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using formatting and <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">Indicates how the output is formatted.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object.
        /// If this is null, default serialization settings will be used.</param>
        /// <returns>
        /// A JSON string representation of the object.
        /// </returns>
        public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
        {
            return SerializeObject(value, null, formatting, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using a type, formatting and <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">Indicates how the output is formatted.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object.
        /// If this is null, default serialization settings will be used.</param>
        /// <param name="type">
        /// The type of the value being serialized.
        /// This parameter is used when <see cref="TypeNameHandling"/> is Auto to write out the type name if the type of the value does not match.
        /// Specifing the type is optional.
        /// </param>
        /// <returns>
        /// A JSON string representation of the object.
        /// </returns>
        public static string SerializeObject(object value, Type type, Formatting formatting, JsonSerializerSettings settings)
        {
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
            jsonSerializer.Formatting = formatting;

            return SerializeObjectInternal(value, type, jsonSerializer);
        }

        private static string SerializeObjectInternal(object value, Type type, JsonSerializer jsonSerializer)
        {
            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = jsonSerializer.Formatting;

                jsonSerializer.Serialize(jsonWriter, value, type);
            }

            return sw.ToString();
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static object DeserializeObject(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value);
        }

        /// <summary>
        /// Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="settings">
        /// The <see cref="JsonSerializerSettings"/> used to deserialize the object.
        /// If this is null, default serialization settings will be used.
        /// </param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static object DeserializeObject(string value, JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="type">The <see cref="Type"/> of object being deserialized.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static object DeserializeObject(string value, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T DeserializeObject<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using a collection of <see cref="JsonConverter"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="converters">Converters to use while deserializing.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, converters);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The object to deserialize.</param>
        /// <param name="settings">
        /// The <see cref="JsonSerializerSettings"/> used to deserialize the object.
        /// If this is null, default serialization settings will be used.
        /// </param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using a collection of <see cref="JsonConverter"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="type">The type of the object to deserialize.</param>
        /// <param name="converters">Converters to use while deserializing.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type, converters);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="type">The type of the object to deserialize to.</param>
        /// <param name="settings">
        /// The <see cref="JsonSerializerSettings"/> used to deserialize the object.
        /// If this is null, default serialization settings will be used.
        /// </param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(value, type, settings ?? JsonSerializerSetting.DefaultJsonSerializerSettings);
        }

        #endregion

    }
}
