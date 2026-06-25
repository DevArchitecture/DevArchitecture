using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class AuthIntegrationTests : BaseIntegrationTest
    {
        [Test]
        public async Task Login_WithoutCredentials_ReturnsUnauthorized()
        {
            var loginData = new { email = "", password = "" };
            var content = new StringContent(
                JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/auth/login");
            request.Headers.Add("x-dev-arch-version", "1.0");
            request.Content = content;

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task SwaggerEndpoint_ReturnsOk()
        {
            var response = await HttpClient.GetAsync("/swagger/v1/swagger.json");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
