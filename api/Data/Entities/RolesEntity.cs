using api.Data.Constants;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace api.Data.Entities
{
    public class RolesEntity : Role
    {
        public virtual ICollection<UsersEntity> Users { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<RolesEntity>().Property(e => e.Name).IsRequired();

            // Seeding

            var role = new RolesEntity()
            {
                Id = Roles.Admin.Id,
                Name = Roles.Admin.Name,
                CreatedAt = Roles.Admin.CreatedAt,
                UpdatedAt = Roles.Admin.UpdatedAt
            };

            modelBuilder.Entity<RolesEntity>().HasData(role);
        }
    }
}
