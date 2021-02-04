﻿using Business.Adapters.SmsService;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Adapters
{
	[TestFixture]
	public class SmsServiceTest
	{
		Mock<ISmsService> smsService;
		SmsServiceHelper smsServiceHelper;

		[SetUp]
		public void Setup()
		{
			smsService = new Mock<ISmsService>();
			smsServiceHelper = new SmsServiceHelper(smsService.Object);

		}

		[Test]
		[TestCase("11111", "test", "123456")]
		public void Send(string password, string text, string cellPhone)
		{
			smsService.Setup(x => x.Send(password, text, cellPhone)).ReturnsAsync(true);
			var result = smsServiceHelper.Send(password, text, cellPhone);
			smsService.Verify(x => x.Send(password, text, cellPhone));

			result.Should().BeTrue();
		}

		[Test]
		[TestCase("test", "123456")]
		public void SendAssist(string text, string cellPhone)
		{
			smsService.Setup(x => x.SendAsist(text, cellPhone)).ReturnsAsync(true);
			var result = smsServiceHelper.SendAsist(text, cellPhone);
			smsService.Verify(x => x.SendAsist(text, cellPhone));

			result.Should().BeTrue();
		}

	}
}
