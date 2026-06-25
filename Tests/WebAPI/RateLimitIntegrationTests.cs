using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class RateLimitIntegrationTests : BaseIntegrationTest
    {
        [Test]
        public async Task Healthz_NotRateLimited()
        {
            // Health check endpoint should not be rate-limited
            for (int i = 0; i < 10; i++)
            {
                var response = await HttpClient.GetAsync("/healthz");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
