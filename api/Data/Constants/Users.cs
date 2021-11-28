using api.Data.Entities;
using api.Data.Services.PasswordHasher;
using Microsoft.Extensions.Configuration;
using System;

namespace api.Data.Constants
{
    public class Users
    {
        public IConfiguration Configuration { get; }

        private readonly IPasswordHasher _passwordHasher;

        public Users(IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            Configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public UsersEntity Admin
        {
            get => new UsersEntity()
            {
                Id = 1,
                Name = Configuration.GetValue<string>("AdminCredentials:Name"),
                Email = Configuration.GetValue<string>("AdminCredentials:Email"),
                Password = _passwordHasher.HashPassword(Configuration.GetValue<string>("AdminCredentials:Password")),
                RoleId = Roles.Admin.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
