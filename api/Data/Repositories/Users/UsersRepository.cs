using JogandoBack.API.Data.Contexts;
using JogandoBack.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JogandoBack.API.Data.Repositories.Users
{
    public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override List<UsersEntity> GetAll()
        {
            return _context.Set<UsersEntity>().Include(u => u.Role).ToList();
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
