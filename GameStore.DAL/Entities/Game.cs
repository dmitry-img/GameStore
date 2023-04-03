using GameStore.DAL.Entities.Common;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Game : BaseDeletableEntity
    {
        public Guid Key { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
        public ICollection<GamePlatformType> GamePlatformTypes { get; set; } = new List<GamePlatformType>();
    }
}
