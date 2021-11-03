namespace api.Data.Services
{
    public interface IUsersService<Response, Request> : IBaseService<Response, Request>
    {
        bool IsUniqueEmail(string email);
    }
}