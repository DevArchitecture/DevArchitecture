using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using WebAPI;

namespace Tests.Helpers.Token;

[TestFixture]
public abstract class BaseIntegrationTest : WebApplicationFactory<Startup>
{
    public string Issuer { get; } = "www.devarchitecture.com";
    public string Audience { get; } = "www.devarchitecture.com";
    public SigningCredentials SigningCredentials { get; }

    protected HttpClient HttpClient { get; set; }

    protected static WebApplicationFactory<Startup> Factory => new();

    [SetUp]
    public void Setup()
    {
        HttpClient = CreateClient();
    }
}
