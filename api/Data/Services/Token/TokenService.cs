using api.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Data.Services.Token
{
    public class TokenService : ITokenService
    {
        public IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(UsersEntity userEntity)
        {
            var key = new SymmetricSecurityKey(GetSecretKey(Configuration));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userEntity.Name),
                new Claim(ClaimTypes.Email, userEntity.Email),
                new Claim(ClaimTypes.Role, userEntity.Role.Name)
            };

            var tokenConfiguration = GetTokenConfiguration(Configuration);

            var token = new JwtSecurityToken(tokenConfiguration.Issuer, tokenConfiguration.Audience, claims, tokenConfiguration.NotBefore,
                tokenConfiguration.Expiration, credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static byte[] GetSecretKey(IConfiguration configuration)
        {
            return Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret"));
        }

        public static TokenConfiguration GetTokenConfiguration(IConfiguration configuration)
        {
            return new TokenConfiguration()
            {
                Issuer = configuration.GetValue<string>("Token:Issuer"),
                Audience = configuration.GetValue<string>("Token:Audience"),
                NotBefore = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddHours(Convert.ToInt32(configuration.GetValue<string>("Token:Expiration")))
            };
        }
    }
}
