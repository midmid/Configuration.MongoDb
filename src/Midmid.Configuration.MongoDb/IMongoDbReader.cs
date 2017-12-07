using System.Collections.Generic;
using MongoDB.Bson;

namespace Midmid.Configuration.MongoDb
{
    public interface IMongoDbReader
    {
        IEnumerable<BsonDocument> FindAll(string collection);
    }
}