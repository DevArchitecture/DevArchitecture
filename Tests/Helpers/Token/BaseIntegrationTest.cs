namespace Tests.Helpers.Token
{
    using global::WebAPI;

    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.IdentityModel.Tokens;
    using NUnit.Framework;

    [TestFixture]
    public abstract class BaseIntegrationTest : WebApplicationFactory<Startup>
    {
        private static readonly JwtSecurityTokenHandler STokenHandler = new ();

        protected HttpClient Client;

        protected WebApplicationFactory<Startup> Factory => new ();

        public string Issuer { get; } = "www.devarchitecture.com";
        public string Audience { get; } = "www.devarchitecture.com";

        public SigningCredentials SigningCredentials { get; }

        [SetUp]
        public void Setup()
        {
            Client = CreateClient();
        }
    }
}