using Microsoft.Extensions.Configuration;

namespace Midmid.Configuration.MongoDb
{
    public class MongoDbConfigurationSource : IConfigurationSource
    {
        public string CollectionName { get; set; }

        public IMongoDbReader MongoDbReader { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MongoDbConfigurationProvider(MongoDbReader, CollectionName);
        }
    }
}