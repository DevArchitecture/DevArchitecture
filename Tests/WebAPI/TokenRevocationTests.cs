using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class TokenRevocationTests : BaseIntegrationTest
    {
        private const string UsersUri = "api/v1/users";
        private const string LogoutUri = "api/v1/auth/logout";

        [Test]
        public async Task Logout_RevokesTheCurrentAccessToken()
        {
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims(), 300);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var logoutResponse = await HttpClient.PostAsync(LogoutUri, null);
            logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await HttpClient.GetAsync(UsersUri);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            // A token issued after the logout must stay usable.
            var freshToken = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims(), 300);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", freshToken);
            var freshResponse = await HttpClient.GetAsync(UsersUri);

            freshResponse.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Token_WithoutJti_IsRejected()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
                issuer: MockJwtTokens.Issuer,
                audience: MockJwtTokens.Audience,
                claims: ClaimsData.GetClaims(),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(300),
                signingCredentials: MockJwtTokens.SigningCredentials);

            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", handler.WriteToken(jwt));

            var response = await HttpClient.GetAsync(UsersUri);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
