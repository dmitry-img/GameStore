using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.CacheEntities;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private const string Cart = "cart";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache<ShoppingCart> _distributedCache;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IDistributedCache<ShoppingCart> distributedCache,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        public async Task<GetOrderDTO> Create(int customerId)
        {
            var shoppingCart = await _distributedCache.GetAsync(Cart);

            if (shoppingCart == null)
            {
                throw new NotFoundException("Shopping cart is empty!");
            }

            var gameKeys = shoppingCart.Items.Select(i => i.GameKey).ToList();

            var games = await _unitOfWork.Games
                .GetQuery()
                .Where(g => gameKeys.Contains(g.Key))
                .ToListAsync();

            var orderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCart.Items)
            {
                var game = games.FirstOrDefault(g => g.Key == item.GameKey);

                if (game == null)
                {
                    throw new NotFoundException(nameof(game), item.GameKey);
                }

                if (item.Quantity > game.UnitsInStock)
                {
                    throw new BadRequestException($"Not enought '{game.Name}' in stock");
                }

                game.UnitsInStock -= item.Quantity;

                var orderDetail = _mapper.Map<OrderDetail>(item);
                orderDetail.GameId = game.Id;
                orderDetails.Add(orderDetail);
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                OrderDetails = orderDetails
            };

            _unitOfWork.Orders.Create(order);

            await _unitOfWork.SaveAsync();

            return new GetOrderDTO
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                TotalSum = order.OrderDetails.Sum(od => od.Price)
            };
        }
    }
}
