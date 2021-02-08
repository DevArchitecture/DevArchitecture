using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using WebAPI;

namespace Tests.Helpers.Token
{
    [TestFixture]
    public abstract class BaseIntegrationTest : WebApplicationFactory<Startup>
    {
        protected HttpClient Client;

        protected WebApplicationFactory<Startup> Factory => new();

        public string Issuer { get; } = "www.devarchitecture.com";
        public string Audience { get; } = "www.devarchitecture.com";

        public SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler STokenHandler = new();


        [SetUp]
        public void Setup()
        {
            Client = CreateClient();
        }
    }
}