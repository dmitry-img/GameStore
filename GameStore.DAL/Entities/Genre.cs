using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> ChildComments { get; set; } = new List<Genre>();
    }
}
