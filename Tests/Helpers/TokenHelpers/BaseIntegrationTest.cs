using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using WebAPI;

namespace Tests.Helpers.TokenHelpers
{
	[TestFixture]
	public abstract class BaseIntegrationTest : WebApplicationFactory<Startup>
	{

		protected HttpClient _client;

		protected WebApplicationFactory<Startup> Factory => new WebApplicationFactory<Startup>();

		public string Issuer { get; } = "www.devarchitecture.com";
		public string Audience { get; } = "www.devarchitecture.com";

		public SigningCredentials SigningCredentials { get; }

		private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();



		[SetUp]
		public void Setup()
		{
			_client = this.CreateClient();
		}
	}
}
