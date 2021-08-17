using Core.Entities.Concrete;
using Core.Extensions;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers;

namespace Tests.Core.Extensions
{
    [TestFixture]
    public class CloneExtensionServiceTests
    {
        [Test]
        public void CloneServiceExtensionTest()
        {
            var user = DataHelper.GetUser("murat");

            var cloneUser = user.Clone();

            cloneUser.Should().BeOfType<User>();
            cloneUser.FullName.Should().Be(user.FullName);
        }
    }
}