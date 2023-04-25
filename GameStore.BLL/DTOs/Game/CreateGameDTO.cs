using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Game
{
    public class CreateGameDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public string PublisherId { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public ICollection<int> PlatformTypeIds { get; set; }
    }
}
