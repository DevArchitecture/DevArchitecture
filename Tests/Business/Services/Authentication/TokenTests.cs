using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Helpers.Token;

namespace Tests.Business.Services.Authentication
{
    [TestFixture]
    public class TokenTests : BaseIntegrationTest
    {
        private const string AuthenticationScheme = "Bearer";
        private const string RequestUri = "api/users/getall";

        [Test]
        public async Task TokenAuthorizeTest()
        {
            // Arrange
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, token);
            var cache = new MemoryCacheManager();

            cache.Add($"{CacheKeys.UserIdForClaim}=1", new List<string>() { "GetUsersQuery" });
            // Act
            var response = await HttpClient.GetAsync(RequestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task TokenExpiredTest()
        {
            const int delayAmount = 10000;

            // Arrange
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, token);

            // Act
            await Task.Delay(delayAmount);

            var response = await HttpClient.GetAsync(RequestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}