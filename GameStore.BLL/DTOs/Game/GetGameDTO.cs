﻿using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class GetGameDTO : BaseGameDTO
    {
        public string Key { get; set; }

        public string PublisherCompanyName { get; set; }

        public ICollection<GetGenreDTO> Genres { get; set; }

        public ICollection<GetPlatformTypeDTO> PlatformTypes { get; set; }
    }
}
