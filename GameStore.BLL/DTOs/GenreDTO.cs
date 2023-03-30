using System.Collections.Generic;

namespace GameStore.BLL.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GenreDTO> ChildGenres { get; set; }
    }
}
