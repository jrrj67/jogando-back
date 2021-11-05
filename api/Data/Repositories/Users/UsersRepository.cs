using api.Data.Contexts;
using api.Data.Entities;
using System.Linq;

namespace api.Data.Repositories.Users
{
    public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool IsUniqueEmail(string email, int userId)
        {
            return !_context.Set<UsersEntity>().Where(u => u.Email == email && u.Id != userId).Any();
        }

        public UsersEntity GetUserByEmail(string email)
        {
            return _context.Set<UsersEntity>().Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
