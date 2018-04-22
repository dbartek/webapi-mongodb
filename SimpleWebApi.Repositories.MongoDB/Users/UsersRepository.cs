using MongoDB.Driver;
using SimpleWebApi.Core.Repositories.Users;
using SimpleWebApi.Repositories.MongoDB.Base;
using System;
using System.Linq;

namespace SimpleWebApi.Repositories.MongoDB.Users
{
    public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
    {
        protected override string CollectionName => "db.users";

        public UsersRepository(MongoDBConfiguration mongoDBConfig) : base(mongoDBConfig)
        {
            var userIndexDefinition = Builders<UserEntity>.IndexKeys.Ascending(r => r.Username);
            var createIndexOptions = new CreateIndexOptions() { Name = "UserUsernameIndexAsc", Background = true, Unique = true };
            EntityCollection.Indexes.CreateOne(userIndexDefinition, createIndexOptions);
        }

        public bool Create(UserEntity userEntity)
        {
            EntityCollection.InsertOne(userEntity);

            return EntityCollection.Count(x => x.Id == userEntity.Id) > 0;
        }

        public UserEntity GetByUsername(string username)
        {
            return EntityCollection.Find(x => x.Username == username).SingleOrDefault();
        }
    }
}
