using Business.Adapters.PersonService;
using Entities.Dtos;
using Moq;
using NUnit.Framework;
using System.Net;

namespace Tests.Business.Adapters
{
	[TestFixture]
	public class PersonManagerTest
	{
		Mock<IPersonService> personService;
		PersonServiceHelper _personServiceHelper;

		[SetUp]
		public void Setup()
		{
			personService = new Mock<IPersonService>();
			_personServiceHelper = new PersonServiceHelper(personService.Object);
		}

		[Test]
		public void VerifyCid_Fail()
		{
			var citizen = new Citizen
			{
				BirthYear = 1987,
				Surname = "Test1",
				Name = "Test1",
				CitizenId = 11111111111

			};
			personService.Setup(x => x.VerifyCid(It.IsAny<Citizen>())).Throws<WebException>();

			var result = _personServiceHelper.VerifyId(citizen);

			Assert.IsFalse(result);
		}


		[Test]
		public void VerifyCid_Success()
		{
			var citizen = new Citizen
			{
				BirthYear = 1987,
				Surname = "Test1",
				Name = "Test1",
				CitizenId = 11111111111

			};

			personService.
							Setup(x => x.VerifyCid(citizen)).ReturnsAsync(true);

			var result = _personServiceHelper.VerifyId(citizen);

			Assert.IsTrue(result);
		}

	}
}
