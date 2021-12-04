using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Filters;
using System.Collections.Generic;

namespace JogandoBack.API.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        bool IsUniqueEmail(string email, int userId);
        UsersEntity GetUserByEmail(string email);
        List<UsersEntity> GetAll(PaginationFilter filter);
    }
}