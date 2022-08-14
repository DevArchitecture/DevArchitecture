using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using Tests.Helpers;
using Tests.Helpers.Token;

namespace Tests.Business.Services.Authentication;

[TestFixture]
public class TokenTests : BaseIntegrationTest
{
    private const string AuthenticationScheme = "Bearer";
    private const string RequestUri = "api/v1/users";

    [Test]
    public async Task TokenAuthorizeTest()
    {
        // Arrange
        var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, token);
        var cache = new MemoryCacheManager();

        cache.Add($"{CacheKeys.UserIdForClaim}=1", new List<string>() { "GetUserQuery", "GetUsersQuery" });
        // Act
        var response = await HttpClient.GetAsync(RequestUri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task TokenExpiredTest()
    {
        // Arrange
        var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims(), 0);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, token);

        var response = await HttpClient.GetAsync(RequestUri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
