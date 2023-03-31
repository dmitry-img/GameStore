using GameStore.DAL.Entities.Common;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Game : BaseDeletableEntity
    {
        public Guid Key { get; set; } = new Guid();
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
        public virtual ICollection<GamePlatformType> GamePlatformTypes { get; set; } = new List<GamePlatformType>();
    }
}
