using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.Data.Configurations
{
    internal class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
           Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }))
                .IsRequired();
        }
    }
}
