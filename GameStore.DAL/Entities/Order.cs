using System;
using System.Collections.Generic;
using GameStore.DAL.Entities.Common;
using GameStore.DAL.Enums;

namespace GameStore.DAL.Entities
{
    public class Order : BaseAuditableEntity
    {
        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public OrderState OrderState { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
