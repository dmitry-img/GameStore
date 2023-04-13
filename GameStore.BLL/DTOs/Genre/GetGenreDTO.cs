using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Genre
{
    public class GetGenreDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}
