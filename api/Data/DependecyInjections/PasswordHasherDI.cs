using JogandoBack.API.Data.Services.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class PasswordHasherDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        }
    }
}
