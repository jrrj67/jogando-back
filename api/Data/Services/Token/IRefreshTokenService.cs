using Microsoft.Extensions.Configuration;

namespace JogandoBack.API.Data.Services.Token
{
    public interface IRefreshTokenService
    {
        IConfiguration Configuration { get; }
        string GenerateToken();
        bool IsValid(string token);
    }
}