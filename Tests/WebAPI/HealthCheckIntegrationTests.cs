using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class HealthCheckIntegrationTests : BaseIntegrationTest
    {
        [Test]
        public async Task Healthz_ReturnsOk()
        {
            var response = await HttpClient.GetAsync("/healthz");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Health_ReturnsOk()
        {
            var response = await HttpClient.GetAsync("/health");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Healthz_ReturnsJson()
        {
            var response = await HttpClient.GetAsync("/healthz");
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("status");
            content.Should().Contain("checks");
        }
    }
}
