using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Enums;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.CacheEntities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private const string Cart = "cart";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache<ShoppingCart> _distributedCache;
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IPaymentStrategyFactory paymentStrategyFactory,
            IDistributedCache<ShoppingCart> distributedCache)
        {
            _unitOfWork = unitOfWork;
            _paymentStrategyFactory = paymentStrategyFactory;
            _distributedCache = distributedCache;
        }

        public async Task<T> ProcessPayment<T>(int orderId, PaymentType paymentType)
        {
            var order = await _unitOfWork.Orders.GetQuery()
                .Include(o => o.OrderDetails.Select(od => od.Game))
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), orderId);
            }

            var strategy = _paymentStrategyFactory.GetStrategy<T>(paymentType);

            var result = strategy.Pay(order);

            if (result == null)
            {
                throw new BadRequestException($"Payment failed!");
            }

            order.Paid = true;

            foreach (var orderDetail in order.OrderDetails)
            {
                var game = orderDetail.Game;
                if (orderDetail.Quantity > game.UnitsInStock)
                {
                    throw new BadRequestException($"Not enought '{game.UnitsInStock}' in stock");
                }

                game.UnitsInStock -= orderDetail.Quantity;
            }

            await _distributedCache.SetAsync(Cart, null);
            await _unitOfWork.SaveAsync();

            return result;
        }
    }
}
