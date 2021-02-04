﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tests.Helpers.TokenHelpers
{
	public static class MockJwtTokens
	{
		public static string Issuer { get; } = "www.devarchitecture.com";
		public static string Audience { get; } = "www.devarchitecture.com";
		public static SecurityKey SecurityKey { get; }
		public static SigningCredentials SigningCredentials { get; }

		private static readonly JwtSecurityTokenHandler STokenHandler = new();
		private static string _keyString = "!z2x3C4v5B*_*!z2x3C4v5B*_*";

		static MockJwtTokens()
		{
			var sKey = Encoding.UTF8.GetBytes(_keyString);
			SecurityKey = new SymmetricSecurityKey(sKey);
			SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

		}

		public static string GenerateJwtToken(IEnumerable<Claim> claims, double value = 5)
		{
			return STokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddSeconds(value), SigningCredentials));
		}
	}
}
