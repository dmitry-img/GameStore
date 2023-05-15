using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class FilterGameDTO
    {
        public List<int> GenreIds { get; set; }

        public List<int> PlatformIds { get; set; }

        public List<int> PublisherIds { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }
    }
}
