using System;
using System.Threading.Tasks;
using CatteryRegister.Tests.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CatteryRegister.Tests
{
    public class WhenCreatingCattery : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WhenCreatingCattery(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task WithAlreadyUsedName_ThenReturnsError()
        {
            // Given
            var catteryName = Guid.NewGuid().ToString();
            await InvokeGraphQlRequestWithVariables(catteryName);
            // When
            var result = await InvokeGraphQlRequestWithVariables(catteryName);
            // Then
            Assert.NotNull(result.errors);
            result.ContainErrorWithCode("cattery-name-already-used");
        }

        private Task<GraphQlResponse> InvokeGraphQlRequestWithVariables(string catteryName)
        {
            return _factory.CreateClient()
                .InvokeGraphQlRequestWithVariables(@"
                mutation createCattery($name:String!){
                    createCattery(name:$name){
                        id
                    }
                }
                ",new
                {
                    name = catteryName
                });
        }
    }
}