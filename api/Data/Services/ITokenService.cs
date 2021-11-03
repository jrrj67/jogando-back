using Microsoft.Extensions.Configuration;
using api.Data.Entities;

namespace api.Data.Services
{
    public interface ITokenService
    {
        IConfiguration Configuration { get; }

        string GenerateToken(UsersEntity userEntity);
    }
}