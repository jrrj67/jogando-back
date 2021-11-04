using api.Data.Entities;
using api.Data.Repositories;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services;
using api.Data.Services.Roles;
using api.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace api.Data.DependecyInjections
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
