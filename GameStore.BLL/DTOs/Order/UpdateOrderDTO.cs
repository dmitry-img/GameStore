using System.Collections.Generic;
using GameStore.DAL.Enums;

namespace GameStore.BLL.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public OrderState OrderState { get; set; }

        public IEnumerable<UpdateOrderDetailDTO> OrderDetails { get; set; }
    }
}
