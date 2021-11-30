using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services.Login;
using JogandoBack.API.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class LoginDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();

            services.AddTransient<ILoginService<LoginResponse, LoginRequest>, LoginService>();
        }
    }
}
