using JogandoBack.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JogandoBack.API.Data.Entities
{
    public class UsersEntity : User
    {
        public RolesEntity Role { get; set; }

        public RefreshTokenEntity RefreshToken { get; set; }

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
        }
    }
}
