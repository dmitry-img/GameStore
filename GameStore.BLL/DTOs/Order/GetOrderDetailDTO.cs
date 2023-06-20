namespace GameStore.BLL.DTOs.Order
{
    public class GetOrderDetailDTO
    {
        public string GameName { get; set; }

        public string GameKey { get; set; }

        public short Quantity { get; set; }
    }
}
