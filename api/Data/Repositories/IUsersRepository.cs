using api.Data.Entities;

namespace api.Data.Repositories
{
    public interface IUsersRepository : IBaseRepository<UsersEntity>
    {
        bool IsUniqueEmail(string email);
    }
}