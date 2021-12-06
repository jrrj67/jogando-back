using JogandoBack.API.Data.Config.Contexts;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Filters.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogandoBack.API.Data.Repositories.Users
{
    public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<UsersEntity> GetAll(UsersFilter usersFilter, PaginationFilter paginationFilter = null)
        {
            var users = _context.Set<UsersEntity>()
                .Include(u => u.Role)
                .ToList();

            if (usersFilter.Name != null)
            {
                users = users.Where(u => u.Name.Contains(usersFilter.Name, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            if (paginationFilter != null)
            {
                return users
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize).ToList();
            }

            return users;
        }

        public override UsersEntity GetById(int id)
        {
            var item = _context.Set<UsersEntity>()
                .Include(u => u.Role)
                .FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException("Not found.");
            }

            return item;
        }

        public bool IsUniqueEmail(string email, int userId)
        {
            return !_context.Set<UsersEntity>()
                .Where(u => u.Email == email && u.Id != userId)
                .Any();
        }

        public UsersEntity GetUserByEmail(string email)
        {
            return _context.Set<UsersEntity>()
                .Include(u => u.Role)
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }
    }
}
