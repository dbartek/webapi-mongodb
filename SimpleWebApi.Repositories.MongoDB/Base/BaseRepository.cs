using MongoDB.Driver;
using SimpleWebApi.Core.Repositories.Base;

namespace SimpleWebApi.Repositories.MongoDB.Base
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoDBConfiguration _mongoDBConfig;

        protected IMongoCollection<TEntity> EntityCollection { get; private set; }

        protected abstract string CollectionName { get; }


        protected BaseRepository(MongoDBConfiguration mongoDBConfig)
        {
            _mongoDBConfig = mongoDBConfig;

            var url = new MongoUrl(_mongoDBConfig.DbConnectionString);
            var client = new MongoClient(url);
            var db = client.GetDatabase(url.DatabaseName);
            EntityCollection = db.GetCollection<TEntity>(CollectionName);
        }
    }
}
