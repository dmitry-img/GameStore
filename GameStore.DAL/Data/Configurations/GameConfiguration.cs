using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.Data.Configurations
{
    internal class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            HasMany(s => s.GameGenres)
            .WithRequired(sc => sc.Game)
            .HasForeignKey(sc => sc.GameId);

            HasMany(s => s.GamePlatformTypes)
            .WithRequired(sc => sc.Game)
            .HasForeignKey(sc => sc.GameId);

            Property(p => p.Key).HasColumnAnnotation(
                "Game_Key_Unique",
                new IndexAnnotation(new[] { new IndexAttribute("Game_Key_Unique") { IsUnique = true } }));
        }
    }
}
