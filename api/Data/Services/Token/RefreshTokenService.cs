using JogandoBack.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        public string GenerateToken(UsersEntity userEntity)
        {
            var key = new SymmetricSecurityKey(_tokenGeneratorService.GetSecretKey(Configuration, "RefreshSecret"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenConfiguration = _tokenGeneratorService.GetTokenConfiguration(Configuration, "RefreshToken");

            return _tokenGeneratorService.GenerateToken(tokenConfiguration.Issuer, tokenConfiguration.Audience, tokenConfiguration.NotBefore,
                tokenConfiguration.Expiration, credentials);
        }
    }
}
