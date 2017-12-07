using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Midmid.Configuration.MongoDb
{
    public class MongoDbConfigurationProvider : ConfigurationProvider
    {
        private readonly IMongoDbReader _mongoDbReader;
        private readonly string _collectionName;

        public MongoDbConfigurationProvider(IMongoDbReader mongoDbReader, string collectionName)
        {
            _mongoDbReader = mongoDbReader ?? throw new ArgumentNullException(nameof(mongoDbReader));
            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
        }

        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string key, value;
            var documents = _mongoDbReader.FindAll(_collectionName);

            foreach (var document in documents)
            {
                key = document[@"Key"].AsString;
                value = document[@"Value"].AsString;

                data[key] = value;
            }

            Data = data;
        }
    }
}