using Core.Extensions;
using NUnit.Framework;

namespace SennedjemTests.Core.Extensions
{
    [TestFixture]
    public class EnumExtensionTests
    {
        [Test]
        public void GetDescriptionTest()
        {
            var description = UserType.Admin.GetDescription();

            Assert.That(description, Is.EqualTo("Admin"));
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
