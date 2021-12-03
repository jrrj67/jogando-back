using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JogandoBack.API.Data.Services.Token
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken(string issuer, string audience, DateTime notBefore, DateTime expiration, SigningCredentials credentials,
            List<Claim> claims = null)
        {
            var token = new JwtSecurityToken(issuer, audience, claims, notBefore,
                expiration, credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public byte[] GetSecretKey(IConfiguration configuration, string key)
        {
            return Encoding.UTF8.GetBytes(configuration.GetValue<string>($"Authentication:{key}Secret"));
        }

        public TokenConfiguration GetTokenConfiguration(IConfiguration configuration, string key)
        {
            return new TokenConfiguration()
            {
                Issuer = configuration.GetValue<string>("Authentication:Issuer"),
                Audience = configuration.GetValue<string>("Authentication:Audience"),
                NotBefore = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(configuration.GetValue<string>($"Authentication:{key}ExpirationInHours")))
            };
        }
    }
}
