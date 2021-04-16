namespace Tests.Helpers.Token
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using global::WebAPI;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.IdentityModel.Tokens;
    using NUnit.Framework;

    [TestFixture]
    public abstract class BaseIntegrationTest : WebApplicationFactory<Startup>
    {
        private static readonly JwtSecurityTokenHandler STokenHandler = new ();

        public string Issuer { get; } = "www.devarchitecture.com";
        public string Audience { get; } = "www.devarchitecture.com";
        public SigningCredentials SigningCredentials { get; }

        protected HttpClient HttpClient { get; set; }

        protected WebApplicationFactory<Startup> Factory => new ();

        [SetUp]
        public void Setup()
        {
            HttpClient = CreateClient();
        }
    }
}