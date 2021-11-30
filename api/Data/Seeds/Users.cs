using JogandoBack.API.Data.Constants;
using JogandoBack.API.Data.Contexts;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Services.PasswordHasher;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace JogandoBack.API.Data.Seeds
{
    public class Users
    {
        public static void Seed(IApplicationBuilder builder, IConfiguration configuration)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                var passwordHasher = serviceScope.ServiceProvider.GetService<IPasswordHasher>();

                var user = new UsersEntity()
                {
                    Name = configuration.GetValue<string>("AdminCredentials:Name"),
                    Email = configuration.GetValue<string>("AdminCredentials:Email"),
                    Password = passwordHasher.HashPassword(configuration.GetValue<string>("AdminCredentials:Password")),
                    RoleId = context.Roles.Where(r => r.Name == RolesConstants.Admin).First().Id
                };

                if (!context.Users.Any())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
