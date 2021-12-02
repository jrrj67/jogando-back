using JogandoBack.API.Data.Entities;

namespace JogandoBack.API.Data.Repositories.RefreshTokens
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshTokenEntity>
    {
        RefreshTokenEntity GetByToken(string token);
    }
}