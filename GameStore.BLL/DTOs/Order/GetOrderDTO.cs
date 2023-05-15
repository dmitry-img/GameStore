namespace GameStore.BLL.DTOs.Order
{
    public class GetOrderDTO
    {
        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public decimal TotalSum { get; set; }
    }
}
