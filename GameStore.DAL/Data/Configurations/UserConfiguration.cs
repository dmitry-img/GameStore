using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Data.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(p => p.Username)
               .HasMaxLength(128)
               .HasColumnAnnotation(
               "Index",
               new IndexAnnotation(new[] { new IndexAttribute("IX_Username") { IsUnique = true } }))
               .IsRequired();

            Property(p => p.Email)
              .HasMaxLength(128)
              .HasColumnAnnotation(
              "Index",
              new IndexAnnotation(new[] { new IndexAttribute("IX_Email") { IsUnique = true } }))
              .IsRequired();
        }
    }
}
