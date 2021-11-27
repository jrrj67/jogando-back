using api.Data.Constants;
using api.Data.Models;
using api.Data.Services.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace api.Data.Entities
{
    public class UsersEntity : User
    {
        public virtual RolesEntity Role { get; set; }

        public IConfiguration Configuration { get; }

        private readonly IPasswordHasher _passwordHasher;

        public UsersEntity()
        {
        }

        public UsersEntity(IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            Configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<UsersEntity>().Property(e => e.Name).IsRequired();

            modelBuilder.Entity<UsersEntity>().Property(e => e.Email).IsRequired();

            modelBuilder.Entity<UsersEntity>().Property(e => e.Password).IsRequired();

            modelBuilder.Entity<UsersEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding

            var userConstant = new Users(Configuration, _passwordHasher);

            var user = new UsersEntity()
            {
                Id = 1,
                Name = userConstant.Admin.Name,
                Email = userConstant.Admin.Email,
                Password = userConstant.Admin.Password,
                CreatedAt = userConstant.Admin.CreatedAt,
                UpdatedAt = userConstant.Admin.UpdatedAt,
                RoleId = 1,
            };

            modelBuilder.Entity<UsersEntity>().HasData(user);
        }
    }
}
