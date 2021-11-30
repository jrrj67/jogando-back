using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace JogandoBack.API.Data.Seeds
{
    public class Seed
    {
        public static void ApplySeed(IApplicationBuilder builder, IConfiguration configuration)
        {
            Roles.Seed(builder);
            Users.Seed(builder, configuration);
        }
    }
}
