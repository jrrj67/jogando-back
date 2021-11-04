using api.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace api.Data.Services.Token
{
    public interface ITokenService
    {
        IConfiguration Configuration { get; }

        string GenerateToken(UsersEntity userEntity);
    }
}