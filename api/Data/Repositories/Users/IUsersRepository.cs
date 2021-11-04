using api.Data.Entities;

namespace api.Data.Repositories.Users
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        bool IsUniqueEmail(string email);
        UsersEntity GetUserByEmail(string email);
    }
}