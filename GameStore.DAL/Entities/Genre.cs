﻿using GameStore.DAL.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Genre : BaseDeletableEntity
    {
        public string Name { get; set; }
        public int? ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }
        public ICollection<Genre> ChildGenres { get; set; } = new List<Genre>();
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
    }
}
