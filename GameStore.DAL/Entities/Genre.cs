using GameStore.DAL.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Genre : BaseAuditableEntity
    {
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }

        public ICollection<Genre> ChildGenres { get; set; } = new List<Genre>();

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
