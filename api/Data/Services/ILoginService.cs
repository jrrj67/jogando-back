namespace api.Data.Services
{
    public interface ILoginService<LoginResponse, LoginRequest>
    {
        LoginResponse Login(LoginRequest userRequest);
    }
}