using JogandoBack.API.Data.Config.Contexts;
using JogandoBack.API.Data.Models.Constants;
using JogandoBack.API.Data.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JogandoBack.API.Data.Models.Seeds
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
