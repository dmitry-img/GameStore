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
           Property(p => p.Name).HasColumnAnnotation(
                "Unique_Genre_Name",
                new IndexAnnotation(new[] { new IndexAttribute("Unique_Genre_Name") { IsUnique = true } }));
        }
    }
}
