using MongoDB.Bson;
using SnowLeopard.Mongo.BaseEntities;
using System;

namespace SnowLeopard.WebApi.MongoEntities
{
    /// <summary>
    /// 来访表
    /// </summary>
    public class Visitor : TopBaseMongoEntity<ObjectId>
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
    }
}
