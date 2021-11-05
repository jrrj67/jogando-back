namespace api.Data.Services.Users
{
    public interface IUsersService<Response, Request> : IBaseService<Response, Request>
    {
        bool IsUniqueEmail(string email, int userId);
    }
}