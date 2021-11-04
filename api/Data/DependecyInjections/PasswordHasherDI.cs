using api.Data.Services.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data.DependecyInjections
{
    public abstract class PasswordHasherDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        }
    }
}
