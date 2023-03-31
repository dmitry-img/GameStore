using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Common;
using GameStore.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace GameStore.DAL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<PlatformType> PlatformTypes { get; set; }
        public virtual DbSet<GameGenre> GameGenres { get; set; }
        public virtual DbSet<GamePlatformType> GamePlatformTypes { get; set; }

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
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry is IDeletable)
                {
                    var deletableEnty = entry as IDeletable;
                    if (entry.State == EntityState.Deleted)
                    {
                        deletableEnty.IsDeleted = true;
                        deletableEnty.DeletedAt = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
