namespace Tests.WebAPI
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using Tests.Helpers;
    using Tests.Helpers.Token;

    [TestFixture]
    public class UsersControllerTests : BaseIntegrationTest
	{
		[Test]
		public async Task GetAll()
		{
			const string authenticationScheme = "Bearer";
			const string requestUri = "api/users/getall";

			// Arrange
			var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);

			// Act
			var response = await Client.GetAsync(requestUri);

			// Assert
			response.StatusCode.Should()?.Be(HttpStatusCode.OK);
		}
	}
}
