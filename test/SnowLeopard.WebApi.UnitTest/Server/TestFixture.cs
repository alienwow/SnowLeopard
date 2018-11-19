using System;
using Xunit;

namespace SnowLeopard.WebApi.UnitTest
{
    /// <summary>
    /// used to startup weiapi, run only once
    /// </summary>
    public class TestFixture : IDisposable
    {
        public ApiServerRunning _server = new ApiServerRunning();

        public TestFixture()
        {
            _server.GivenRunningOn("http://localhost:33052");
        }
        private void Connect()
        {
            Console.WriteLine("webapi startup successfully!!! ");
        }

        public void Dispose()
        {
        }
    }

    /// <summary>
    /// used to tag every class using test fixture
    /// </summary>
    [CollectionDefinition("TestCollection")]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
    }
}
