using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using api.Data.Repositories;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services;
using api.Data.Validators;

namespace api.Data.DependecyInjections
{
    public abstract class UsersDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<UsersRequest>, UsersValidator>();

            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IUsersService<UsersResponse, UsersRequest>, UsersService>();
        }
    }
}
