namespace GameStore.BLL.DTOs.Game
{
    public class BaseGameDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }
    }
}
