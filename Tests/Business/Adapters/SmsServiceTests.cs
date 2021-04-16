namespace Tests.Business.Adapters
{
    using FluentAssertions;
    using global::Business.Adapters.SmsService;
    using Moq;
    using NUnit.Framework;
    using Tests.Helpers.Adapter;

    [TestFixture]
    public class SmsServiceTests
	{
        private Mock<ISmsService> _smsService;
        private SmsServiceHelper _smsServiceHelper;

        [SetUp]
        public void Setup()
		{
			_smsService = new Mock<ISmsService>();
			_smsServiceHelper = new SmsServiceHelper(_smsService.Object);
		}

        [Test]
        [TestCase("11111", "test", "123456")]
        public void Send(string password, string text, string cellPhone)
		{
			// Arrange
			_smsService.Setup(x => x.Send(password, text, cellPhone)).ReturnsAsync(true);

			// Act
			var result = _smsServiceHelper.Send(password, text, cellPhone);
			_smsService.Verify(x => x.Send(password, text, cellPhone));

			// Assert
			result.Should().BeTrue();
		}

        [Test]
        [TestCase("test", "123456")]
        public void SendAssist(string text, string cellPhone)
		{
			// Arrange
			_smsService.Setup(x => x.SendAssist(text, cellPhone)).ReturnsAsync(true);

			// Act
			var result = _smsServiceHelper.SendAssist(text, cellPhone);
			_smsService.Verify(x => x.SendAssist(text, cellPhone));

			// Assert
			result.Should().BeTrue();
		}

	}
}
