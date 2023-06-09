using System;
using System.Collections.Generic;
using GameStore.DAL.Enums;

namespace GameStore.BLL.DTOs.Order
{
    public class GetOrderDTO
    {
        public string CustomerId { get; set; }

        public string CustomerUsername { get; set; }

        public int OrderId { get; set; }

        public OrderState OrderState { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal TotalSum { get; set; }

        public IEnumerable<GetOrderDetailDTO> OrderDetails { get; set; }
    }
}
