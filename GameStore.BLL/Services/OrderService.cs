using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.CacheEntities;
using GameStore.DAL.Entities;
using GameStore.DAL.Enums;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private const string Cart = "cart";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache<ShoppingCart> _distributedCache;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public OrderService(
            IUnitOfWork unitOfWork,
            IDistributedCache<ShoppingCart> distributedCache,
            IMapper mapper,
            ILog logger)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrderDTO> CreateAsync(string userObjectId)
        {
            var shoppingCart = await _distributedCache.GetAsync($"{Cart}:{userObjectId}");

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

            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.ObjectId == userObjectId);

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                OrderDetails = orderDetails,
                OrderState = OrderState.Unpaid,
                User = user
            };

            _unitOfWork.Orders.Create(order);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Order({order.Id}) has been created!");

            return _mapper.Map<GetOrderDTO>(order);
        }

        public async Task<PaginationResult<GetOrderDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            var orders = await _unitOfWork.Orders
                .GetQuery()
                .Include(o => o.OrderDetails.Select(od => od.Game))
                .Include(o => o.User)
                .Where(o =>
                    o.OrderDate >= oneMonthAgo &&
                    (o.OrderState == OrderState.Paid || o.OrderState == OrderState.Shipped))
                .OrderByDescending(o => o.Id)
                .ToListAsync();

            return PaginationResult<GetOrderDTO>.ToPaginationResult(
                    _mapper.Map<IEnumerable<GetOrderDTO>>(orders),
                    paginationDTO.PageNumber,
                    paginationDTO.PageSize);
        }

        public async Task<GetOrderDTO> GetAsync(int orderId)
        {
            var order = await _unitOfWork.Orders
                .GetQuery()
                .Include(o => o.OrderDetails.Select(od => od.Game))
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), orderId);
            }

            return _mapper.Map<GetOrderDTO>(order);
        }

        public async Task UpdateAsync(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _unitOfWork.Orders
                .GetQuery()
                .Include(o => o.OrderDetails.Select(od => od.Game))
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), orderId);
            }

            if (updateOrderDTO.OrderState == OrderState.Shipped && order.OrderState != OrderState.Shipped)
            {
                order.ShippedDate = DateTime.UtcNow;
            }
            else if (updateOrderDTO.OrderState != OrderState.Shipped && order.OrderState == OrderState.Shipped)
            {
                order.ShippedDate = null;
            }

            _mapper.Map(updateOrderDTO, order);

            var updatedOrderDetails = updateOrderDTO.OrderDetails.ToList();

            for (int i = 0; i < updatedOrderDetails.Count; i++)
            {
                var orderDetail = order.OrderDetails
                    .FirstOrDefault(od => od.Game.Key == updatedOrderDetails[i].GameKey);

                if (orderDetail != null)
                {
                    if (updatedOrderDetails[i].Quantity < orderDetail.Game.UnitsInStock)
                    {
                        _mapper.Map(updatedOrderDetails[i], orderDetail);
                    }
                    else
                    {
                        throw new BadRequestException($"Not enough '{orderDetail.Game.Name}' in stock!");
                    }
                }
                else
                {
                    throw new NotFoundException($"Order details with game key ({updatedOrderDetails[i].GameKey}) " +
                        $"was not found!");
                }
            }

            await _unitOfWork.SaveAsync();

            _logger.Info($"Order({order.Id}) has been updated!");
        }
    }
}
