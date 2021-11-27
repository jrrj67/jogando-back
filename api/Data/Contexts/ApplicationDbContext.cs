using api.Data.Entities;
using api.Data.Models;
using api.Data.Services.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace api.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<RolesEntity> Roles { get; set; }

        private readonly IPasswordHasher _passwordHasher;

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration, IPasswordHasher passwordHasher) : base(options)
        {
            Configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("Default"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var usersEntity = new UsersEntity(Configuration, _passwordHasher);
            usersEntity.OnModelCreating(modelBuilder);

            RolesEntity.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        // Logic implemented to add created at and updated at

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAt = now;
                }

                ((BaseModel)entity.Entity).UpdatedAt = now;
            }
        }
    }
}