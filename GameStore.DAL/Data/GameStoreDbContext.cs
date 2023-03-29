using GameStore.DAL.Entities;
using System.Data.Entity;

namespace GameStore.DAL.Data
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<Game> Games { get; private set; }
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<Genre> Genres { get; private set; }
        public DbSet<PlatformType> PlatformTypes { get; private set; }
        public DbSet<GameGenre> GameGenres { get; private set; }
        public DbSet<GamePlatformType> GamePlatformTypes { get; private set; }

        public GameStoreDbContext(string connectionString)
           : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e1 => e1.Genres)
                .WithMany(e2 => e2.Games)
                .Map(m =>
                {
                    m.ToTable("GameGenres");
                    m.MapLeftKey("GameId");
                    m.MapRightKey("GenreId");
                });

            modelBuilder.Entity<Game>()
                .HasMany(e1 => e1.PlatformTypes)
                .WithMany(e2 => e2.Games)
                .Map(m =>
                {
                    m.ToTable("GamePlatformTypes");
                    m.MapLeftKey("GameId");
                    m.MapRightKey("PlatformTypeId");
                });
        }

    }
}
