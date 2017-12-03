using Microsoft.Extensions.Configuration;

namespace Midmid.Configuration.MongoDb
{
    public class MongoDbConfigurationSource : IConfigurationSource
    {
        public string ConnectionString { get; set; }

        public string CollectionName { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MongoDbConfigurationProvider(ConnectionString, CollectionName);
        }
    }
}