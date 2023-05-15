using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Strategies
{
    public class BankPaymentStrategy : IPaymentStrategy<MemoryStream>
    {
        public MemoryStream Pay(Order order)
        {
            var data = $"The order {order.Id} has been successully created!{Environment.NewLine}" +
                $"Total price: {order.OrderDetails.Sum(od => od.Price)}";
            return new MemoryStream(Encoding.ASCII.GetBytes(data));
        }
    }
}
