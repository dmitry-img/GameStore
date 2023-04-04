using GameStore.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.Data.Configurations
{
    internal class GameGenreConfiguration : EntityTypeConfiguration<GameGenre>
    {
        public GameGenreConfiguration()
        {
           HasKey(sc => new { sc.GameId, sc.GenreId });
        }
    }
}
