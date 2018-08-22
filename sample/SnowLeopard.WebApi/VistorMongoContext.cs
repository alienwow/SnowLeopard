using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SnowLeopard.Mongo;
using SnowLeopard.WebApi.MongoEntities;

namespace SnowLeopard.WebApi
{
    /// <summary>
    /// VistorMongoContext
    /// </summary>
    public class VistorMongoContext : MongoContext
    {
        public VistorMongoContext(IOptions<GlobalConfig> options) : base(options.Value.MongoOption)
        {
        }

        public IMongoCollection<Visitor> Visitors => DataBase.GetCollection<Visitor>("visitors");
    }
}
