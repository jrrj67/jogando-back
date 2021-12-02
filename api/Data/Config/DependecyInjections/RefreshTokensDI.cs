using FluentValidation;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Services.RefreshTokensEntityService;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
{
    public abstract class RefreshTokensDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RefreshTokensRequest>, RefreshTokensValidator>();

            services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();

            services.AddTransient<IRefreshTokensEntityService<RefreshTokensResponse, RefreshTokensRequest>, RefreshTokensEntityService>();
        }
    }
}
