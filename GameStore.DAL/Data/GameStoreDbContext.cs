using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Common;
using GameStore.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.DAL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatformType> GamePlatformTypes { get; set; }

        public GameStoreDbContext() : base("name=DefaultConnection")
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ApplyDeletableInformation();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<GameGenre>()
           .HasKey(sc => new { sc.GameId, sc.GenreId });

            modelBuilder.Entity<Game>()
                .HasMany(s => s.GameGenres)
                .WithRequired(sc => sc.Game)
                .HasForeignKey(sc => sc.GameId);

            modelBuilder.Entity<Genre>()
                .HasMany(c => c.GameGenres)
                .WithRequired(sc => sc.Genre)
                .HasForeignKey(sc => sc.GenreId);

            modelBuilder.Entity<GamePlatformType>()
           .HasKey(sc => new { sc.GameId, sc.PlatformTypeId });

            modelBuilder.Entity<Game>()
                .HasMany(s => s.GamePlatformTypes)
                .WithRequired(sc => sc.Game)
                .HasForeignKey(sc => sc.GameId);

            modelBuilder.Entity<PlatformType>()
                .HasMany(c => c.GamePlatformTypes)
                .WithRequired(sc => sc.PlatformType)
                .HasForeignKey(sc => sc.PlatformTypeId);
        }

        private void ApplyDeletableInformation()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var deletableEnty = entry.Entity as IDeletable;
                if (deletableEnty != null)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        deletableEnty.IsDeleted = true;
                        deletableEnty.DeletedAt = DateTime.UtcNow;

                        entry.State = EntityState.Modified;
                        return;
                    }
                }
            }
        }
    }
}
