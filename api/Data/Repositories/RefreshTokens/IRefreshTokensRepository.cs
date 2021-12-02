using JogandoBack.API.Data.Models.Entities;

namespace JogandoBack.API.Data.Repositories.RefreshTokens
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshTokensEntity>
    {
        RefreshTokensEntity GetByToken(string token);
        RefreshTokensEntity GetByUserId(int id);
    }
}