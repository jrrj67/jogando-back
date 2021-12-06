using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Filters.Users;
using System.Collections.Generic;

namespace JogandoBack.API.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        List<UsersEntity> GetAll(UsersFilter usersFilter, PaginationFilter paginationFilter = null);
        bool IsUniqueEmail(string email, int userId);
        UsersEntity GetUserByEmail(string email);
    }
}