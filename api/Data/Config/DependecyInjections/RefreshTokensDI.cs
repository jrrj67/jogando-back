using FluentValidation;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
{
    public abstract class RefreshTokensDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RefreshTokensBaseRequest>, RefreshTokensValidator>();

            services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();
        }
    }
}
