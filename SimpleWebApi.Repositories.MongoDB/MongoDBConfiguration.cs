
using SimpleWebApi.Core;

namespace SimpleWebApi.Repositories.MongoDB
{
    public class MongoDBConfiguration
    {
        private readonly AppConfiguration _appConfiguration;

        public MongoDBConfiguration(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public string DbConnectionString => _appConfiguration.DbConnectionString;
    }
}
