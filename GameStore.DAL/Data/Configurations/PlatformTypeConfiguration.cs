using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.Data.Configurations
{
    internal class PlatformTypeConfiguration : EntityTypeConfiguration<PlatformType>
    {
        public PlatformTypeConfiguration()
        {
            Property(p => p.Type)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .HasColumnAnnotation(
                 "Index",
                 new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }))
                .IsRequired();
        }
    }
}
