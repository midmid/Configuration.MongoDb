using System;
using Midmid.Configuration.MongoDb;

namespace Microsoft.Extensions.Configuration
{
    public static class MongoDbConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, string connectionString)
        {
            return AddMongoDb(configurationBuilder, new DefaultMongoDbReader(connectionString), "AppSettings");
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, string connectionString, string collectionName)
        {
            return AddMongoDb(configurationBuilder, new DefaultMongoDbReader(connectionString), collectionName);
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, IMongoDbReader mongoDbReader)
        {
            return AddMongoDb(configurationBuilder, mongoDbReader, "AppSettings");
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, IMongoDbReader mongoDbReader, string collectionName)
        {
            if (mongoDbReader == null)
            {
                throw new ArgumentNullException(nameof(mongoDbReader));
            }

            if (collectionName == null)
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            configurationBuilder.Add(new MongoDbConfigurationSource { MongoDbReader = mongoDbReader, CollectionName = collectionName });
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder builder, Action<MongoDbConfigurationSource> configureSource)
            => builder.Add(configureSource);
    }
}