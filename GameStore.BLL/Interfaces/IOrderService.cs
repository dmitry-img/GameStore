using System.Threading.Tasks;
using GameStore.BLL.DTOs.Order;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<GetOrderDTO> Create(int customerId);
    }
}
