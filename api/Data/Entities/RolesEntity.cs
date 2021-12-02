using JogandoBack.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JogandoBack.API.Data.Entities
{
    public class RolesEntity : Role
    {
        public ICollection<UsersEntity> Users { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<RolesEntity>().Property(e => e.Name).IsRequired();
        }
    }
}
