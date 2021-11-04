using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Login;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data.DependecyInjections
{
    public abstract class LoginDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<ILoginService<LoginResponse, LoginRequest>, LoginService>();
        }
    }
}
