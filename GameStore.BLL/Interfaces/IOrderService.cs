using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.DAL.Entities;
using GameStore.DAL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<GetOrderDTO> CreateAsync();

        Task<PaginationResult<GetOrderDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO);

        Task UpdateAsync(int orderId, UpdateOrderDTO updateOrderDTO);

        Task<GetOrderDTO> GetAsync(int orderId);
    }
}
