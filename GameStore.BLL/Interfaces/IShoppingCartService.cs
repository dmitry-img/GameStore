using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.ShoppingCart;

namespace GameStore.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddItemAsync(CreateShoppingCartItemDTO cartItemDTO);

        Task<IEnumerable<GetShoppingCartItemDTO>> GetAllItemsAsync();

        Task DeleteItemAsync(string gameKey);

        Task<int> GetGameQuantityByKeyAsync(string gameKey);
    }
}
