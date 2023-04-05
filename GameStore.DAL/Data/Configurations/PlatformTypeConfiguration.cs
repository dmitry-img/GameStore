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
            Property(p => p.Type).HasColumnAnnotation(
                 "Unique_Platform_Type",
                 new IndexAnnotation(new[] { new IndexAttribute("Unique_Platform_Type") { IsUnique = true } }));
        }
    }
}
