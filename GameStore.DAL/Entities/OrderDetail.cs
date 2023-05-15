using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Entities
{
    public class OrderDetail : BaseAuditableEntity
    {
        public int GameId { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public Game Game { get; set; }
    }
}
