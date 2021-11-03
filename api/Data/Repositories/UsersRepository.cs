using System.Linq;
using api.Data.Contexts;
using api.Data.Entities;

namespace api.Data.Repositories
{
    public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool IsUniqueEmail(string email)
        {
            return !_context.Set<UsersEntity>().Where(u => u.Email == email).Any();
        }
    }
}
