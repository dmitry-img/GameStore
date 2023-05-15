using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentStrategy<T>
    {
        T Pay(Order order);
    }
}
