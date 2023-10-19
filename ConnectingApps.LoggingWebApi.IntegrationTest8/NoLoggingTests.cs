using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ConnectingApps.LoggingWebApi.IntegrationTest7
{
    public  class NoLoggingTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public NoLoggingTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ReadInTestOutputIfNothingIsLogged()
        {
            var response = await _client.GetAsync("/weatherforecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
