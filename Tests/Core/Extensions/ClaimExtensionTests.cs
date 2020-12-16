using Core.Extensions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SennedjemTests.Core.Extensions
{
    [TestFixture]
    public class ClaimExtensionTests
    {
        List<Claim> claimList;
        string[] roles;

        [SetUp]
        public void Setup()
        {
            claimList = new List<Claim>();
            roles = new string[3] { "Admin", "User", "MasterUser" };
        }

        [Test]
        [TestCase("test@test.com")]
        public void AddEmail(string email)
        {
            claimList.AddEmail(email);
            Assert.That(claimList.Count(x => x.Type == JwtRegisteredClaimNames.Email && x.Value == email), Is.EqualTo(1));
        }

        [Test]
        [TestCase("testname")]
        public void AddName(string name)
        {
            claimList.AddName(name);
            Assert.That(claimList.Count(x => x.Type == ClaimTypes.Name && x.Value == name), Is.EqualTo(1));
        }

        [Test]
        [TestCase("testIdentifier")]
        public void AddNameIdentifier(string identifier)
        {
            claimList.AddNameIdentifier(identifier);
            Assert.That(claimList.Count(x => x.Type == ClaimTypes.NameIdentifier && x.Value == identifier), Is.EqualTo(1));
        }

        [Test]
        [TestCase("testNameUniqueIdentifier")]
        public void NameUniqueIdentifier(string name)
        {
            claimList.AddNameUniqueIdentifier(name);
            Assert.That(claimList.Count(x => x.Type == ClaimTypes.SerialNumber && x.Value == name), Is.EqualTo(1));

        }

        [Test]
        public void AddRoles()
        {
            claimList.AddRoles(roles);
            Assert.That(claimList.Count(x => x.Type == ClaimTypes.Role), Is.EqualTo(3));
        }

    }
}
