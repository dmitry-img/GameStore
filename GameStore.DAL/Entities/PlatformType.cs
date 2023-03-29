using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class PlatformType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
