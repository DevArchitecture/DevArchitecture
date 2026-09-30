using System;
using Core.Utilities.Security.Jwt;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Core.Security.Jwt
{
    [TestFixture]
    public class TokenOptionsValidatorTests
    {
        private const string DevelopmentKey = "!z2x3C4v5B*_*!z2x3C4v5B*_*!z2x3C4v5B*_*";
        private const string DockerSmokeKey = "DockerSmokeJwtSigningKey_MustBeLongEnoughForHmacSha256_AlsoNotForProductionUse";

        [Test]
        public void Validate_MissingTokenOptions_Throws()
        {
            Action act = () => TokenOptionsValidator.Validate(null, "Production");

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Validate_EmptySecurityKey_Throws()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = ""
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Production");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*TokenOptions__SecurityKey*");
        }

        [Test]
        public void Validate_ShortSecurityKey_Throws()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = "too-short-key"
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Production");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*32*");
        }

        [Test]
        public void Validate_PubliclyKnownKeyInProduction_Throws()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = DevelopmentKey
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Production");

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*TokenOptions__SecurityKey*");
        }

        [Test]
        public void Validate_DockerSmokeKeyInProduction_Throws()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = DockerSmokeKey
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Production");

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Validate_PubliclyKnownKeyInStaging_Throws()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = DevelopmentKey
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Staging");

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Validate_PubliclyKnownKeyInDevelopment_DoesNotThrow()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = DevelopmentKey
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Development");

            act.Should().NotThrow();
        }

        [Test]
        public void Validate_UniqueKeyInProduction_DoesNotThrow()
        {
            var tokenOptions = new TokenOptions
            {
                Issuer = "www.devarchitecture.com",
                Audience = "www.devarchitecture.com",
                AccessTokenExpiration = 10,
                SecurityKey = "d41d8cd98f00b204e9800998ecf8427e9a1b3c5d7e2f4a6b8c0d1e2f3a4b5c6d"
            };

            Action act = () => TokenOptionsValidator.Validate(tokenOptions, "Production");

            act.Should().NotThrow();
        }
    }
}
