using JogandoBack.API.Data.Services.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
{
    public abstract class PasswordHasherDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        }
    }
}
