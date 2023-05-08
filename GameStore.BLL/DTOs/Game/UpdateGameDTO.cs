using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class UpdateGameDTO : BaseGameDTO
    {
        public ICollection<int> GenreIds { get; set; }

        public ICollection<int> PlatformTypeIds { get; set; }
    }
}
