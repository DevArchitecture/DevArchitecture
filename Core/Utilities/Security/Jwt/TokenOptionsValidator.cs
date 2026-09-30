using System;
using System.Linq;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    /// <summary>
    /// Validates <see cref="TokenOptions"/> at startup so that deployments cannot run
    /// with a missing, too short or publicly known JWT signing key.
    /// </summary>
    public static class TokenOptionsValidator
    {
        private const int MinimumSecurityKeyLengthInBytes = 32; // Use at least a 256-bit key for HMAC-SHA256.

        private static readonly string[] PubliclyKnownSecurityKeys =
        {
            "!z2x3C4v5B*_*!z2x3C4v5B*_*!z2x3C4v5B*_*",
            "DockerSmokeJwtSigningKey_MustBeLongEnoughForHmacSha256_AlsoNotForProductionUse"
        };

        /// <summary>
        /// Throws <see cref="InvalidOperationException"/> when the configured signing key is unusable
        /// or publicly known for the given environment.
        /// </summary>
        public static void Validate(TokenOptions tokenOptions, string environmentName)
        {
            if (tokenOptions == null || string.IsNullOrWhiteSpace(tokenOptions.SecurityKey))
            {
                throw new InvalidOperationException(
                    "TokenOptions:SecurityKey is not configured. Generate a cryptographically random key and " +
                    "provide it through a protected configuration source such as the " +
                    "TokenOptions__SecurityKey environment variable.");
            }

            var keyLengthInBytes = Encoding.UTF8.GetByteCount(tokenOptions.SecurityKey);
            if (keyLengthInBytes < MinimumSecurityKeyLengthInBytes)
            {
                throw new InvalidOperationException(
                    $"TokenOptions:SecurityKey must be at least {MinimumSecurityKeyLengthInBytes} bytes long " +
                    $"for HMAC-SHA256. Current size: {keyLengthInBytes} bytes.");
            }

            if (IsDeploymentEnvironment(environmentName) && PubliclyKnownSecurityKeys.Contains(tokenOptions.SecurityKey))
            {
                throw new InvalidOperationException(
                    $"TokenOptions:SecurityKey is a publicly known sample key and cannot be used in the " +
                    $"{environmentName} environment. Generate a unique key and provide it through the " +
                    "TokenOptions__SecurityKey environment variable.");
            }
        }

        private static bool IsDeploymentEnvironment(string environmentName)
        {
            return string.Equals(environmentName, "Production", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(environmentName, "Staging", StringComparison.OrdinalIgnoreCase);
        }
    }
}
