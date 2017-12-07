using Microsoft.Extensions.Configuration;
using Xunit;

namespace Midmid.Configuration.MongoDb.Test
{
    public class MongoDbConfigurationFunctionalTest
    {
        [Fact]
        public void Configure_and_load_settings()
        {
            var reader = new MongoDbReaderBuilder()
                .AddSetting("setting1", "value1")
                .AddSetting("setting2", "value2")
                .Build();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddMongoDb(reader);

            var config = configurationBuilder.Build();

            Assert.Equal("value1", config["setting1"]);
            Assert.Equal("value2", config["setting2"]);
        }
    }
}