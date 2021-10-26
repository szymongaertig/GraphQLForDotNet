using System.Threading.Tasks;
using CatteryRegister.Tests.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CatteryRegister.Tests
{
    public class WhenGettingGraphQlSchema : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WhenGettingGraphQlSchema(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ThenReturnsSuccessResponse()
        {
            Assert.True(await _factory.CreateClient().ContainsValidGraphQlSchema());
        }
    }
}