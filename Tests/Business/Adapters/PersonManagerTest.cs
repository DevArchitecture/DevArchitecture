using Business.Adapters.PersonService;
using Moq;
using NUnit.Framework;
using System.Net;

namespace SennedjemTests.Business.Adapters
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
        [TestCase(11111111111, "Test1", "Test1", 1987)]
        public void VerifyCid_Fail(long TCKimlikNo, string Ad, string Soyad, int DogumYili)
        {
            personService.Setup(x => x.VerifyCid(
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()
            )).Throws<WebException>();

            var result = _personServiceHelper.VerifyId(TCKimlikNo, Ad, Soyad, DogumYili);

            Assert.IsFalse(result);

        }


        [Test]
        [TestCase(11111111111, "Test1", "Test1", 1987)]
        public void VerifyCid_Success(long TCKimlikNo, string Ad, string Soyad, int DogumYili)
        {

            personService.
                Setup(x => x.VerifyCid(TCKimlikNo, Ad, Soyad, DogumYili)).ReturnsAsync(true);

            var result = _personServiceHelper.VerifyId(TCKimlikNo, Ad, Soyad, DogumYili);

            Assert.IsTrue(result);
        }

    }
}
