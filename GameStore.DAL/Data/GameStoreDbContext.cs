using System;
using GameStore.DAL.Data.Configurations;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.DAL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

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
        }

        private void ApplyDeletableInformation()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is IDeletable deletableEntry)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        deletableEntry.IsDeleted = true;
                        deletableEntry.DeletedAt = DateTime.UtcNow;

                        entry.State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
