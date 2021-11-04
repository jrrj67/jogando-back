namespace api.Data.Services.Login
{
    public interface ILoginService<LoginResponse, LoginRequest>
    {
        LoginResponse Login(LoginRequest userRequest);
        bool VerifyEmailAndPassword(string email, string password);
    }
}