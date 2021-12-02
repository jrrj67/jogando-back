using JogandoBack.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace JogandoBack.API.Data.Services.Token
{
    public class TokenService : ITokenService
    {
        public IConfiguration Configuration { get; }
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public TokenService(IConfiguration configuration, ITokenGeneratorService tokenGeneratorService)
        {
            Configuration = configuration;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public string GenerateToken(UsersEntity userEntity)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userEntity.Name),
                new Claim(ClaimTypes.Email, userEntity.Email),
                new Claim(ClaimTypes.Role, userEntity.Role.Name)
            };

            var key = new SymmetricSecurityKey(_tokenGeneratorService.GetSecretKey(Configuration, "Token"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenConfiguration = _tokenGeneratorService.GetTokenConfiguration(Configuration, "Token");

            return _tokenGeneratorService.GenerateToken(tokenConfiguration.Issuer, tokenConfiguration.Audience, tokenConfiguration.NotBefore,
                tokenConfiguration.Expiration, credentials, claims);
        }
    }
}
