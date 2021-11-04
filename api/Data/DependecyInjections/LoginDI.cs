using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Login;
using api.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data.DependecyInjections
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
