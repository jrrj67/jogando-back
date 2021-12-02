using FluentValidation;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services;
using JogandoBack.API.Data.Services.RefreshToken;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class RefreshTokenDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RefreshTokenRequest>, RefreshTokenValidator>();

            services.AddTransient<IBaseService<RefreshTokenResponse, RefreshTokenRequest>, RefreshTokenService>();
        }
    }
}
