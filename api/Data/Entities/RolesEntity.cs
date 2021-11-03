using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using api.Data.Models;

namespace api.Data.Entities
{
    public class RolesEntity : Role
    {
        public virtual ICollection<UsersEntity> Users { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<RolesEntity>().Property(e => e.Name).IsRequired();
        }
    }
}
