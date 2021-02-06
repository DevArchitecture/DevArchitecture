using Core.Entities.Concrete;
using Core.Extensions;
using NUnit.Framework;
using Tests.Helpers;


namespace Tests.Core.Extensions
{
	[TestFixture]
	public class CloneExtesionServiceTest
	{
		[Test]
		public void CloneServiceExtensionTest()
		{
			var user = DataHelper.GetUser("murat");

			var cloneUser = user.Clone();

			Assert.That(cloneUser, Is.TypeOf(typeof(User)));
			Assert.That(cloneUser.FullName, Is.EqualTo(user.FullName));

		}

	}
}
