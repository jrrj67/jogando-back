using JogandoBack.API.Data.Entities;

namespace JogandoBack.API.Data.Repositories.RefreshTokens
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshTokensEntity>
    {
        RefreshTokensEntity GetByToken(string token);
    }
}