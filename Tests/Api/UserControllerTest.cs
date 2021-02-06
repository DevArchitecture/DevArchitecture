using NUnit.Framework;
using Tests.Helpers;
using Tests.Helpers.TokenHelpers;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;

namespace Tests.Api
{
	[TestFixture]
	public class UserControllerTest : BaseIntegrationTest
	{
		[Test]
		public async Task GetAll()
		{
			var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await Client.GetAsync("api/users/getall");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

		}
	}
}
