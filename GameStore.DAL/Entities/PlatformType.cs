using GameStore.DAL.Entities.Common;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class PlatformType : BaseDeletableEntity
    {
        public string Type { get; set; }
        public virtual ICollection<GamePlatformType> GamePlatformTypes { get; set; } = new List<GamePlatformType>();
    }
}
