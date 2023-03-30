using GameStore.DAL.Entities.Common;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Genre : BaseDeletableEntity
    {
        public string Name { get; set; }
        public int? ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> ChildGenres { get; set; } = new List<Genre>();
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
