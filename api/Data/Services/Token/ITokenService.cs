using JogandoBack.API.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace JogandoBack.API.Data.Services.Token
{
    public interface ITokenService
    {
        IConfiguration Configuration { get; }

        string GenerateToken(UsersEntity userEntity);
    }
}