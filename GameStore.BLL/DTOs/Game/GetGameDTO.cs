using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class GetGameDTO
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public string PublisherCompanyName { get; set; }

        public ICollection<GetGenreDTO> Genres { get; set; }

        public ICollection<GetPlatformTypeDTO> PlatformTypes { get; set; }
    }
}
