﻿using JogandoBack.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JogandoBack.API.Data.Entities
{
    public class RefreshTokensEntity : RefreshToken
    {
        public UsersEntity User { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokensEntity>().HasKey(rt => rt.Id);

            modelBuilder.Entity<RefreshTokensEntity>().Property(rt => rt.Token).IsRequired();

            modelBuilder.Entity<RefreshTokensEntity>()
                .HasOne(rt => rt.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshTokensEntity>(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}