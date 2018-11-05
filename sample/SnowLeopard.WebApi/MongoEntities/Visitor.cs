using System;
using MongoDB.Bson;
using SnowLeopard.Caching.Abstractions;
using SnowLeopard.Mongo.BaseEntities;

namespace SnowLeopard.WebApi.MongoEntities
{
    /// <summary>
    /// 来访表
    /// </summary>
    public class Visitor : TopBaseMongoEntity<ObjectId>, ICachable
    {
        /// <summary>
        /// UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 来访人Id
        /// </summary>
        public long VisitorId { get; set; }

        /// <summary>
        /// 来访时间
        /// </summary>
        public DateTime VisitTime { get; set; }

        public string CacheKey => Id.ToString();
    }
}
