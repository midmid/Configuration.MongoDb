using System;
using Xunit;

namespace Midmid.Configuration.MongoDb.Test
{
    public class MongoDbConfigurationProviderTest
    {
        [Fact]
        public void Ctor_should_throw_with_null_connectionstring()
        {
            Assert.Throws<ArgumentNullException>(() => new MongoDbConfigurationProvider(null, @"collection"));
        }
    }
}