#nullable enable
using System.Net;
using Business.Adapters.PersonService;
using Entities.Dtos;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tests.Helpers.Adapter;

namespace Tests.Business.Adapters
{
    [TestFixture]
    public class PersonManagerTests
    {
        private const int BirthYear = 1987;
        private const string Surname = "Test1";
        private const string Name = "Test1";
        private const long CitizenId = 11111111111;

        private Mock<IPersonService>? _personService;
        private PersonServiceHelper? _personServiceHelper;

        [SetUp]
        public void Setup()
        {
            _personService = new Mock<IPersonService>();
            _personServiceHelper = new PersonServiceHelper(_personService.Object);
        }

        [Test]
        public void VerifyCid_Fail()
        {
            // Arrange
            var citizen = CreateCitizen();

            _personService?.Setup(x => x.VerifyCid(It.IsAny<Citizen>())).Throws<WebException>();

            // Act
            var result = _personServiceHelper?.VerifyId(citizen);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void VerifyCid_Success()
        {
            // Arrange
            var citizen = CreateCitizen();

            _personService?.Setup(x => x.VerifyCid(citizen)).ReturnsAsync(true);

            // Act
            var result = _personServiceHelper?.VerifyId(citizen);

            // Assert
            result.Should().BeTrue();
        }

        private static Citizen CreateCitizen(
            int birthYear = BirthYear,
            string surname = Surname,
            string name = Name,
            long citizenId = CitizenId)
        {
            return new ()
            {
                BirthYear = birthYear,
                Surname = surname,
                Name = name,
                CitizenId = citizenId
            };
        }
    }
}