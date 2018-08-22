using MongoDB.Driver;

namespace SnowLeopard.Mongo
{
    public abstract partial class MongoContext
    {
        /// <summary>
        /// mongoOption
        /// </summary>
        protected readonly MongoOption _mongoOption;

        /// <summary>
        /// Client
        /// </summary>
        protected MongoClient Client { get; set; }

        /// <summary>
        /// DataBase
        /// </summary>
        protected IMongoDatabase DataBase { get; set; }

        /// <summary>
        /// MongoContext
        /// </summary>
        /// <param name="mongoOption"></param>
        public MongoContext(MongoOption mongoOption)
        {
            _mongoOption = mongoOption;
            Client = new MongoClient(new MongoUrl(_mongoOption.ConnectionString));
            DataBase = Client.GetDatabase(_mongoOption.DataBase);
        }

    }
}
