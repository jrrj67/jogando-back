using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace JogandoBack.API.Data.Services.Token
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public IConfiguration Configuration { get; }
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public RefreshTokenService(IConfiguration configuration, ITokenGeneratorService tokenGeneratorService)
        {
            Configuration = configuration;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public string GenerateToken()
        {
            var key = new SymmetricSecurityKey(_tokenGeneratorService.GetSecretKey(Configuration, "RefreshToken"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenConfiguration = _tokenGeneratorService.GetTokenConfiguration(Configuration, "RefreshToken");

            return _tokenGeneratorService.GenerateToken(tokenConfiguration.Issuer, tokenConfiguration.Audience, tokenConfiguration.NotBefore,
                tokenConfiguration.Expiration, credentials);
        }

        public bool IsValid(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenConfiguration = _tokenGeneratorService.GetTokenConfiguration(Configuration, "Refresh");

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(_tokenGeneratorService.GetSecretKey(Configuration, "Token")),
                ValidIssuer = tokenConfiguration.Issuer,
                ValidAudience = tokenConfiguration.Audience,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
