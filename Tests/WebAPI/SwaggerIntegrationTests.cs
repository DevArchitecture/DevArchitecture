using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class SwaggerIntegrationTests : BaseIntegrationTest
    {
        [Test]
        public async Task SwaggerJson_ContainsApiVersion()
        {
            var response = await HttpClient.GetAsync("/swagger/v1/swagger.json");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("openapi");
            content.Should().Contain("DevArchitecture API");
        }

        [Test]
        public async Task SwaggerJson_ContainsJwtSecurity()
        {
            var response = await HttpClient.GetAsync("/swagger/v1/swagger.json");
            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Bearer");
            content.Should().Contain("JWT");
        }

        [Test]
        public async Task SwaggerJson_DefinesAllEndpoints()
        {
            var response = await HttpClient.GetAsync("/swagger/v1/swagger.json");
            var content = await response.Content.ReadAsStringAsync();

            var expectedEndpoints = new[]
            {
                "Auth", "Users", "Groups",
                "Languages", "Translates"
            };

            foreach (var endpoint in expectedEndpoints)
            {
                content.Should().Contain(endpoint);
            }
        }
    }
}
