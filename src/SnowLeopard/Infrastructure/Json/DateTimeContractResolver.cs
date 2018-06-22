using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// DateTimeContractResolver
    /// </summary>
    public class DateTimeContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// ResolveContractConverter
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (objectType != typeof(DateTime) && objectType != typeof(DateTime?))
            {
                return base.ResolveContractConverter(objectType);
            }

            return new UnixTimestampConvert();
        }
    }

}
