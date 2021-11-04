using api.Data.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data.DependecyInjections
{
    public abstract class TokenDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
        }
    }
}
