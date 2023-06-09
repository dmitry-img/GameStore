using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Api.Interfaces;
using GameStore.DAL.Data.Configurations;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Data
{
    public class GameStoreDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public GameStoreDbContext(ICurrentUserService currentUserService) : base("name=DefaultConnection")
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Role { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ApplyDeletableInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new PlatformTypeConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
        }

        private void ApplyDeletableInformation()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditableEntity auditableEntity)
                {
                    var userObjectId = _currentUserService.GetCurrentUserObjectId();
                    if (entry.State == EntityState.Added && auditableEntity.CreatedAt == null)
                    {
                        auditableEntity.CreatedAt = DateTime.UtcNow;
                        auditableEntity.CreatedBy = userObjectId;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        auditableEntity.ModifiedAt = DateTime.UtcNow;
                        auditableEntity.ModifiedBy = userObjectId;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        auditableEntity.IsDeleted = true;
                        auditableEntity.DeletedAt = DateTime.UtcNow;
                        auditableEntity.DeletedBy = userObjectId;

                        entry.State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
