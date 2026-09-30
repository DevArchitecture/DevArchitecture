using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class UsersPaginationTests : BaseIntegrationTest
    {
        private const string RequestUri = "api/v1/users";

        [Test]
        public async Task GetList_WithPageSizeOne_ReturnsSingleItemAndFullTotalCount()
        {
            var token = await LoginAsync();
            var beforeTotal = await GetTotalRecordsAsync(token);
            await CreateUserAsync(token);

            using var request = new HttpRequestMessage(HttpMethod.Get, $"{RequestUri}?pageNumber=1&pageSize=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("x-dev-arch-version", "1.0");
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            doc.RootElement.GetProperty("data").GetArrayLength().Should().Be(1);
            doc.RootElement.GetProperty("totalRecords").GetInt32().Should().Be(beforeTotal + 1);
        }

        [Test]
        public async Task GetList_WithSecondPage_ReturnsNextItemWithoutOverlap()
        {
            var token = await LoginAsync();
            await CreateUserAsync(token);

            var firstPage = await GetPageAsync(token, 1, 1);
            var secondPage = await GetPageAsync(token, 2, 1);

            firstPage.GetProperty("data").GetArrayLength().Should().Be(1);
            secondPage.GetProperty("data").GetArrayLength().Should().Be(1);
            var firstId = firstPage.GetProperty("data")[0].GetProperty("userId").GetInt32();
            var secondId = secondPage.GetProperty("data")[0].GetProperty("userId").GetInt32();
            firstId.Should().NotBe(secondId);
        }

        private async Task<string> LoginAsync()
        {
            var content = new StringContent(
                "{\"email\":\"admin@adminmail.com\",\"password\":\"Q1w212*_*\"}",
                Encoding.UTF8,
                "application/json");
            var response = await HttpClient.PostAsync("api/v1/auth/login", content);
            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return doc.RootElement.GetProperty("data").GetProperty("token").GetString();
        }

        private async Task<JsonElement> GetPageAsync(string token, int pageNumber, int pageSize)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get,
                $"{RequestUri}?pageNumber={pageNumber}&pageSize={pageSize}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("x-dev-arch-version", "1.0");
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return doc.RootElement.Clone();
        }

        private async Task<int> GetTotalRecordsAsync(string token)
        {
            var page = await GetPageAsync(token, 1, 100);
            return page.GetProperty("totalRecords").GetInt32();
        }

        private async Task CreateUserAsync(string token)
        {
            var content = new StringContent(
                $"{{\"fullName\":\"Pagination Test\",\"email\":\"{Guid.NewGuid():N}@test.com\"," +
                "\"password\":\"Q1w212*_*\",\"status\":true}",
                Encoding.UTF8,
                "application/json");
            using var request = new HttpRequestMessage(HttpMethod.Post, RequestUri)
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("x-dev-arch-version", "1.0");
            var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Create failed: {(int)response.StatusCode} {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
