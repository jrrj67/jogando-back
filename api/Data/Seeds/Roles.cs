using api.Data.Constants;
using api.Data.Contexts;
using api.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace api.Data.Seeds
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
