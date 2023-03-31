using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Genre
{
    public class GetGenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetGenreDTO> ChildGenres { get; set; }
    }
}
