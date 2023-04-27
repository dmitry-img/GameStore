namespace GameStore.BLL.DTOs.ShoppingCart
{
    public class CreateShoppingCartItemDTO
    {
        public string GameKey { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public short Quantity { get; set; }
    }
}
