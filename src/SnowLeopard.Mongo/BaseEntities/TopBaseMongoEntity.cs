using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SnowLeopard.Mongo.BaseEntities
{
    /// <summary>
    /// TopBaseMongoEntity
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TopBaseMongoEntity<TKey>
    {
        /// <summary>
        /// Mongo Id
        /// </summary>
        [BsonId]
        public TKey Id { get; set; }
    }
}
