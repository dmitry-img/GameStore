using System;
using System.Collections.Generic;
using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Entities
{
    public class Order : BaseDeletableEntity
    {
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
