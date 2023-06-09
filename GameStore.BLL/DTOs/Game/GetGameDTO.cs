using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.DTOs.Publisher;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class GetGameDTO : BaseGameDTO
    {
        public string Key { get; set; }

        public GetPublisherBriefDTO Publisher { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<GetGenreDTO> Genres { get; set; }

        public ICollection<GetPlatformTypeDTO> PlatformTypes { get; set; }
    }
}
