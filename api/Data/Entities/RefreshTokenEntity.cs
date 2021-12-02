using JogandoBack.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JogandoBack.API.Data.Entities
{
    public class RefreshTokenEntity : RefreshToken
    {
        public virtual UsersEntity User { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenEntity>().HasKey(rt => rt.Id);

            modelBuilder.Entity<RefreshTokenEntity>().Property(rt => rt.Token).IsRequired();

            modelBuilder.Entity<RefreshTokenEntity>()
                .HasOne(rt => rt.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshTokenEntity>(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
