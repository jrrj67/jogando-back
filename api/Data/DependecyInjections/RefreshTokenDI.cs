using FluentValidation;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services.RefreshTokensEntityService;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class RefreshTokenDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RefreshTokenRequest>, RefreshTokenValidator>();

            services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();

            services.AddTransient<IRefreshTokensEntityService<RefreshTokenResponse, RefreshTokenRequest>, RefreshTokensEntityService>();
        }
    }
}
