using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using WebAPI;

namespace Tests.Helpers.Token
{
    [TestFixture]
    public abstract class BaseIntegrationTest
    {
        private WebApplicationFactory<Startup> _factory;

        protected HttpClient HttpClient { get; set; }

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
            HttpClient = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            HttpClient?.Dispose();
            _factory?.Dispose();
        }
    }
}