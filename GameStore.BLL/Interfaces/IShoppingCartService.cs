using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.ShoppingCart;

namespace GameStore.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddItemAsync(CreateShoppingCartItemDTO cartItemDTO);

        Task<List<GetShoppingCartItemDTO>> GetAllItemsAsync();

        Task DeleteItem(string gameKey);
    }
}
