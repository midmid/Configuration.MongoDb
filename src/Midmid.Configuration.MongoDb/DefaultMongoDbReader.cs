using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Midmid.Configuration.MongoDb
{
    public class DefaultMongoDbReader : IMongoDbReader
    {
        private readonly IMongoDatabase _database;

        public DefaultMongoDbReader(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var mongoUrl = MongoUrl.Create(connectionString);
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
            var client = new MongoClient(clientSettings);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IEnumerable<BsonDocument> FindAll(string collectionName)
        {
            if (collectionName == null)
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders<BsonDocument>.Filter.Empty).ToEnumerable();
        }
    }
}