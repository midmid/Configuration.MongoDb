using System;
using Midmid.Configuration.MongoDb;

namespace Microsoft.Extensions.Configuration
{
    public static class MongoDbConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, string connectionString)
        {
            return AddMongoDb(configurationBuilder, connectionString, "AppSettings");
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder configurationBuilder, string connectionString, string collectionName)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (collectionName == null)
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            configurationBuilder.Add(new MongoDbConfigurationSource { ConnectionString = connectionString, CollectionName = collectionName });
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddMongoDb(this IConfigurationBuilder builder, Action<MongoDbConfigurationSource> configureSource)
            => builder.Add(configureSource);
    }
}