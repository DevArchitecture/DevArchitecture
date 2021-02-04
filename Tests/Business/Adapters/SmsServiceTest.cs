﻿using Business.Adapters.SmsService;
using FluentAssertions;
using Moq;
using NUnit.Framework;


namespace Tests.Business.Adapters
{
	[TestFixture]
	public class SmsServiceTest
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
			_smsService.Setup(x => x.Send(password, text, cellPhone)).ReturnsAsync(true);
			var result = _smsServiceHelper.Send(password, text, cellPhone);
			_smsService.Verify(x => x.Send(password, text, cellPhone));
			result.Should().BeTrue();
		}

		[Test]
		[TestCase("test", "123456")]
		public void SendAssist(string text, string cellPhone)
		{
			_smsService.Setup(x => x.SendAsist(text, cellPhone)).ReturnsAsync(true);
			var result = _smsServiceHelper.SendAssist(text, cellPhone);
			_smsService.Verify(x => x.SendAsist(text, cellPhone));
			result.Should().BeTrue();
		}

	}
}
