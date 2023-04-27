using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.CacheEntities;
using GameStore.DAL.Interfaces;
using log4net;
using log4net.Core;

namespace GameStore.BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDistributedCache<ShoppingCart> _distributedCache;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public ShoppingCartService(IDistributedCache<ShoppingCart> distributedCache, IMapper mapper, ILog logger)
        {
            _distributedCache = distributedCache;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddItemAsync(CreateShoppingCartItemDTO cartItemDTO)
        {
            var newItem = _mapper.Map<ShoppingCartItem>(cartItemDTO);

            var cacheKey = "cart";

            var shoppingCart = await _distributedCache.GetAsync(cacheKey);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { Items = new List<ShoppingCartItem>() };
            }

            var item = shoppingCart.Items.FirstOrDefault(i => i.GameKey == newItem.GameKey);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                shoppingCart.Items.Add(newItem);
            }

            await _distributedCache.SetAsync(cacheKey, shoppingCart);
            _logger.Info($"{cartItemDTO.GameName} was added to shopping cart!");
        }

        public async Task DeleteItemAsync(string gameKey)
        {
            var cacheKey = $"cart";

            var shoppingCart = await _distributedCache.GetAsync(cacheKey);

            if (shoppingCart == null)
            {
                throw new NotFoundException(nameof(shoppingCart), cacheKey);
            }

            var item = shoppingCart.Items.FirstOrDefault(i => i.GameKey == gameKey);

            item.Quantity--;

            if (item.Quantity <= 0)
            {
                shoppingCart.Items.Remove(item);
            }

            await _distributedCache.SetAsync(cacheKey, shoppingCart);
            _logger.Info($"{item.GameName} was deleted from shopping cart!");
        }

        public async Task<IEnumerable<GetShoppingCartItemDTO>> GetAllItemsAsync()
        {
            var cacheKey = $"cart";

            var shoppingCart = await _distributedCache.GetAsync(cacheKey);

            if (shoppingCart == null)
            {
                throw new NotFoundException(nameof(shoppingCart), cacheKey);
            }

            return _mapper.Map<List<GetShoppingCartItemDTO>>(shoppingCart.Items);
        }
    }
}
