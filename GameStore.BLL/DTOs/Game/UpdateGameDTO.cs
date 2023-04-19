using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class UpdateGameDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public ICollection<int> PlatformTypeIds { get; set; }
    }
}
