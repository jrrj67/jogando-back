using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Repositories;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services;
using JogandoBack.API.Data.Services.Roles;
using JogandoBack.API.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class RolesDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RolesRequest>, RolesValidator>();

            services.AddTransient<IBaseRepository<RolesEntity>, BaseRepository<RolesEntity>>();

            services.AddTransient<IBaseService<RolesResponse, RolesRequest>, RolesService>();
        }
    }
}
