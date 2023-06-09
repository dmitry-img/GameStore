﻿using System.Collections.Generic;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.Enums;

namespace GameStore.BLL.DTOs.Game
{
    public class FilterGameDTO
    {
        public string NameFragment { get; set; }

        public List<int> GenreIds { get; set; }

        public List<int> PlatformTypeIds { get; set; }

        public List<int> PublisherIds { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public DateFilterOption DateFilterOption { get; set; }

        public SortOption SortOption { get; set; }

        public PaginationDTO Pagination { get; set; }
    }
}
