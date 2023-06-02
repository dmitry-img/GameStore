using GameStore.DAL.Entities.Common;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Game : BaseAuditableEntity
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }

        public int Views { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public ICollection<PlatformType> PlatformTypes { get; set; } = new List<PlatformType>();
    }
}
