using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SnowLeopard.Mongo.BaseEntities
{
    /// <summary>
    /// TopBaseMongoEntity
    /// </summary>
    public partial class TopBaseMongoEntity<TKey>
    {
        /// <summary>
        /// Mongo Id
        /// </summary>
        [BsonId]
        public TKey Id { get; set; }
    }

    public class TopBaseMongoEntity : TopBaseMongoEntity<ObjectId>
    {

    }
}
