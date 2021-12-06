using FluentValidation;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Repositories.Roles;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
{
    public abstract class RolesDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RolesRequest>, RolesValidator>();

            services.AddTransient<IRolesRepository, RolesRepository>();
        }
    }
}
