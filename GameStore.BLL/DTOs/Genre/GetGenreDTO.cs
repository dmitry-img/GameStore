using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Genre
{
    public class GetGenreDTO : BaseGenreDTO
    {
        public int Id { get; set; }

        public string ParentGenreName { get; set; }
    }
}
