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
            HasMany(s => s.Genres)
            .WithMany(g => g.Games);

            HasMany(s => s.PlatformTypes)
            .WithMany(g => g.Games);

            Property(p => p.Key)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }))
                .IsRequired(); ;
        }
    }
}
