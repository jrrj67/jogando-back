using JogandoBack.API.Data.Models.Entities;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Services.Login
{
    public interface ILoginService<LoginResponse, LoginRequest>
    {
        Task<LoginResponse> Login(LoginRequest userRequest);
        bool VerifyEmailAndPassword(string email, string password);
        Task<LoginResponse> Authenticate(UsersEntity userEntity);
    }
}