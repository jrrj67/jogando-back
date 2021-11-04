using api.Data.Repositories.Users;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Users;
using api.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
