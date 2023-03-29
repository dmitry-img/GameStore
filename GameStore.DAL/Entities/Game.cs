using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
        public virtual ICollection<PlatformType> PlatformTypes { get; set; } = new HashSet<PlatformType>();
    }
}
