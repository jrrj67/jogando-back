using JogandoBack.API.Data.Models.Entities;

namespace JogandoBack.API.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        bool IsUniqueEmail(string email, int userId);
        UsersEntity GetUserByEmail(string email);
    }
}