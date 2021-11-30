using JogandoBack.API.Data.Entities;

namespace JogandoBack.API.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        bool IsUniqueEmail(string email, int userId);
        UsersEntity GetUserByEmail(string email);
    }
}