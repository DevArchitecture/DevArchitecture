using Core.Entities.Concrete;
using Core.Extensions;
using NUnit.Framework;
using SennedjemTests.Helpers;


namespace SennedjemTests.Core.Extensions
{
    [TestFixture]
    public class CloneExtesionServiceTest
    {
        [Test]
        public void CloneServiceExtensionTest()
        {
            var _user = DataHelper.GetUser("murat");

            var cloneUser = _user.Clone();

            Assert.That(cloneUser, Is.TypeOf(typeof(User)));
            Assert.That(cloneUser.FullName, Is.EqualTo(_user.FullName));

        }

    }
}
