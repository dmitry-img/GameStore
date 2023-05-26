using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using Unity;

namespace GameStore.BLL.Factories
{
    public class PaymentStrategyFactory : IPaymentStrategyFactory
    {
        private readonly IUnityContainer _container;

        public PaymentStrategyFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IPaymentStrategy<T> GetStrategy<T>(PaymentType paymentType)
        {
            return _container.Resolve<IPaymentStrategy<T>>(paymentType.ToString());
        }
    }
}
