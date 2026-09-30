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
    public class ForgedTokenLifetimeTests : BaseIntegrationTest
    {
        private const string RequestUri = "api/v1/users";

        [Test]
        public async Task Token_WithinMaxLifetime_IsNotRejectedByLifetimeValidation()
        {
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims(), 300);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetAsync(RequestUri);

            response.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Token_WithOneYearLifetime_IsRejected()
        {
            var token = GenerateLongLivedToken(TimeSpan.FromDays(365));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetAsync(RequestUri);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private static string GenerateLongLivedToken(TimeSpan lifetime)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
                issuer: MockJwtTokens.Issuer,
                audience: MockJwtTokens.Audience,
                claims: ClaimsData.GetClaims(),
                notBefore: DateTime.UtcNow.AddSeconds(-30),
                expires: DateTime.UtcNow.Add(lifetime),
                signingCredentials: MockJwtTokens.SigningCredentials);
            return handler.WriteToken(jwt);
        }
    }
}
