using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Data.Configurations
{
    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(p => p.Name)
              .HasMaxLength(128)
              .HasColumnAnnotation(
              "Index",
              new IndexAnnotation(new[] { new IndexAttribute("IX_Name") { IsUnique = true } }))
              .IsRequired();
        }
    }
}
