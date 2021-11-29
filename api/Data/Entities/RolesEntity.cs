using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace api.Data.Entities
{
    public class RolesEntity : Role
    {
        public virtual ICollection<UsersEntity> Users { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<RolesEntity>().Property(e => e.Name).IsRequired();
        }
    }
}
