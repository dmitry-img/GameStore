using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.ShoppingCart;

namespace GameStore.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddItemAsync(string userObjectId, CreateShoppingCartItemDTO cartItemDTO);

        Task<IEnumerable<GetShoppingCartItemDTO>> GetAllItemsAsync(string userObjectId);

        Task DeleteItemAsync(string userObjectId, string gameKey);

        Task<int> GetGameQuantityByKeyAsync(string userObjectId, string gameKey);
    }
}
