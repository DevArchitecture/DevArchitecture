using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Core.Extensions
{
    [TestFixture]
    public class ClaimExtensionTests
    {
        private List<Claim> _claimList;
        private string[] _roles;

        [SetUp]
        public void Setup()
        {
            _claimList = new List<Claim>();
            _roles = new string[3] { "Admin", "User", "MasterUser" };
        }

        [Test]
        [TestCase("test@test.com")]
        public void AddEmail(string email)
        {
            _claimList.AddEmail(email);
            _claimList.Where(x => x.Type == JwtRegisteredClaimNames.Email && x.Value == email).Should().HaveCount(1);
        }

        [Test]
        [TestCase("testname")]
        public void AddName(string name)
        {
            _claimList.AddName(name);
            _claimList.Where(x => x.Type == ClaimTypes.Name && x.Value == name).Should().HaveCount(1);
        }

        [Test]
        [TestCase("testIdentifier")]
        public void AddNameIdentifier(string identifier)
        {
            _claimList.AddNameIdentifier(identifier);
            _claimList.Where(x => x.Type == ClaimTypes.NameIdentifier && x.Value == identifier).Should().HaveCount(1);
        }

        [Test]
        [TestCase("testNameUniqueIdentifier")]
        public void NameUniqueIdentifier(string name)
        {
            _claimList.AddNameUniqueIdentifier(name);
            _claimList.Where(x => x.Type == ClaimTypes.SerialNumber && x.Value == name).Should().HaveCount(1);
        }

        [Test]
        public void AddRoles()
        {
            _claimList.AddRoles(_roles);
            _claimList.Where(x => x.Type == ClaimTypes.Role).Should().HaveCount(3);
        }
    }
}