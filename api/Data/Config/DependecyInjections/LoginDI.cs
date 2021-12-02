using FluentValidation;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Services.Login;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
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
