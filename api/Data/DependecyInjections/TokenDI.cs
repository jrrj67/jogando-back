using JogandoBack.API.Data.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class TokenDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();

            // Refresh token
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();

            // Token generator
            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
        }
    }
}
