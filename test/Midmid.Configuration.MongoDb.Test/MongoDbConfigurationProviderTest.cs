using System;
using Moq;
using Xunit;

namespace Midmid.Configuration.MongoDb.Test
{
    public class MongoDbConfigurationProviderTest
    {
        [Fact]
        public void Ctor_should_throw_with_null_mongodb_reader()
        {
            Assert.Throws<ArgumentNullException>(() => new MongoDbConfigurationProvider(null, "settings"));
        }

        [Fact]
        public void Ctor_should_throw_with_null_collection()
        {
            var reader = new Mock<IMongoDbReader>().Object;
            Assert.Throws<ArgumentNullException>(() => new MongoDbConfigurationProvider(reader, null));
        }

        [Fact]
        public void Load_should_set_data()
        {
            var reader = new MongoDbReaderBuilder()
                .AddSetting("setting1", "value1")
                .AddSetting("setting2", "value2")
                .Build();

            var provider = new MongoDbConfigurationProvider(reader, "settings");
            provider.Load();

            Assert.True(provider.TryGet("setting1", out var value1));
            Assert.True(provider.TryGet("setting2", out var value2));
            Assert.False(provider.TryGet("setting3", out var value3));
            Assert.Equal("value1", value1);
            Assert.Equal("value2", value2);
            Assert.True(provider.TryGet("SeTTing1", out value1));
            Assert.Equal("value1", value1);
        }
    }
}