using GameStore.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.Data.Configurations
{
    internal class GamePlatformTypeConfiguration : EntityTypeConfiguration<GamePlatformType>
    {
        public GamePlatformTypeConfiguration()
        {
            HasKey(sc => new { sc.GameId, sc.PlatformTypeId });
        }
    }
}
