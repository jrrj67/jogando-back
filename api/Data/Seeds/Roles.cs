using JogandoBack.API.Data.Constants;
using JogandoBack.API.Data.Contexts;
using JogandoBack.API.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JogandoBack.API.Data.Seeds
{
    public class Roles
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                var role = new RolesEntity()
                {
                    Name = RolesConstants.Admin
                };

                if (!context.Roles.Any())
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
            }
        }
    }
}
