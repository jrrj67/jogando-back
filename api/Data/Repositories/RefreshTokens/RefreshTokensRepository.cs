using JogandoBack.API.Data.Config.Contexts;
using JogandoBack.API.Data.Models.Entities;
using System.Linq;

namespace JogandoBack.API.Data.Repositories.RefreshTokens
{
    public class RefreshTokensRepository : BaseRepository<RefreshTokensEntity>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(ApplicationDbContext context) : base(context)
        {
        }

        public RefreshTokensEntity GetByToken(string token)
        {
            return _context.RefreshTokens.Where(rt => rt.Token == token).FirstOrDefault();
        }
        
        public RefreshTokensEntity GetByUserId(int id)
        {
            return _context.RefreshTokens.Where(rt => rt.UserId == id).FirstOrDefault();
        }
    }
}
