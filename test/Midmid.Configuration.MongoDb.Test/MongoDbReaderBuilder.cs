using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Moq;

namespace Midmid.Configuration.MongoDb.Test
{
    public class MongoDbReaderBuilder
    {
        private List<BsonDocument> _documents = new List<BsonDocument>();

        public IMongoDbReader Build()
        {
            var reader = new Mock<IMongoDbReader>(MockBehavior.Strict);
            reader.Setup(r => r.FindAll(It.IsAny<string>()))
                .Returns(_documents);
            return reader.Object;
        }

        public MongoDbReaderBuilder AddSetting(string key, string value)
        {
            _documents.Add(GenerateBsonDocument(key, value));
            return this;
        }

        private BsonDocument GenerateBsonDocument(string key, string value)
        {
            var document = $"{{ Key : \"{key}\", Value: \"{value}\" }}";
            return BsonSerializer.Deserialize<BsonDocument>(document);
        }
    }
}