using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentStrategyFactory
    {
        IPaymentStrategy<T> GetStrategy<T>(PaymentType paymentType);
    }
}
