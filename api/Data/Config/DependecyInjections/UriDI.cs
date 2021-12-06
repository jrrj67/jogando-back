using JogandoBack.API.Data.Services.Uri;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.Config.DependecyInjections
{
    public abstract class UriDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }
    }
}
