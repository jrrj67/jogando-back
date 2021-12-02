using FluentValidation;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class RefreshDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<RefreshRequest>, RefreshValidator>();
        }
    }
}
