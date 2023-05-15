using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Strategies
{
    public class IBoxPaymentStrategy : IPaymentStrategy<int>
    {
        public int Pay(Order order)
        {
            return order.Id;
        }
    }
}
