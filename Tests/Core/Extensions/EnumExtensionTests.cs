using Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Core.Extensions
{
	[TestFixture]
	public class EnumExtensionTests
	{
		[Test]
		public void GetDescriptionTest()
		{
			var description = UserType.Admin.GetDescription();

			description.Should().Be("Admin");
		}
	}

	public enum UserType
	{
		[System.ComponentModel.Description("Admin")]
		Admin = 1,

		[System.ComponentModel.Description("Guess")]
		Guess = 2,

		[System.ComponentModel.Description("Office")]
		Office = 3
	}
}
