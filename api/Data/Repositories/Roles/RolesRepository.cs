using JogandoBack.API.Data.Config.Contexts;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Repositories.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Repositories.Roles
{
    public class RolesRepository : BaseRepository<RolesEntity>, IRolesRepository
    {
        private readonly IUsersRepository _usersRepository;

        public RolesRepository(ApplicationDbContext context, IUsersRepository usersRepository) : base(context)
        {
            _usersRepository = usersRepository;
        }

        public override Task DeleteAsync(int id)
        {
            var users = _usersRepository.GetAll();

            if (users.Where(u => u.RoleId == id).Any())
            {
                throw new Exception("Can't delete role because it has a user using it.");
            }

            return base.DeleteAsync(id);
        }
    }
}
