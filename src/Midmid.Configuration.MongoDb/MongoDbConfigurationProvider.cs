using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Midmid.Configuration.MongoDb
{
    public class MongoDbConfigurationProvider : ConfigurationProvider
    {
        //mongodb://user:pass@hostname/db1?authSource=userDb
        private readonly string _collectionName;
        private readonly IMongoDatabase _database;

        public MongoDbConfigurationProvider(string connectionString, string collectionName)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));

            var mongoUrl = MongoUrl.Create(connectionString);
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
            var client = new MongoClient(clientSettings);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string key, value;
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            var documents = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

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