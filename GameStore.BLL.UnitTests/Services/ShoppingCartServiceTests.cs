using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.CacheEntities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class ShoppingCartServiceTests : BaseTest
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartServiceTests()
        {
            _shoppingCartService = new ShoppingCartService(MockShoppingCartCash.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task AddItemAsync_WhichShoppingCartNotContain_ShouldAddItemToShoppingCart()
        {
            // Arrange
            var cartKey = "cart";

            var shoppingCartItem = new CreateShoppingCartItemDTO
            {
                GameName = "Warcraft IV",
                GameKey = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
            };

            // Act
            await _shoppingCartService.AddItemAsync(shoppingCartItem);

            // Assert
            var usershoppingCart = await MockShoppingCartCash.Object.GetAsync(cartKey);
            Assert.Equal(3, usershoppingCart.Items.Count);
            MockShoppingCartCash.Verify(sc => sc.SetAsync(It.IsAny<string>(), It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Fact]
        public async Task AddItemAsync_WhichShoppingCartContains_ShouldIncreaseQuantity()
        {
            // Arrange
            var cartKey = "cart";

            var shoppingCartItem = new CreateShoppingCartItemDTO
            {
                GameName = "Warcraft III",
                GameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                GamePrice = 10,
                Quantity = 1
            };

            // Act
            await _shoppingCartService.AddItemAsync(shoppingCartItem);

            // Assert
            var usershoppingCart = await MockShoppingCartCash.Object.GetAsync(cartKey);
            var item = usershoppingCart.Items.SingleOrDefault(i => i.GameName == "Warcraft III");
            Assert.Equal(2, usershoppingCart.Items.Count);
            Assert.Equal(2, item.Quantity);
            MockShoppingCartCash.Verify(sc => sc.SetAsync(It.IsAny<string>(), It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_WithQuantityOne_ShouldDeleteItem()
        {
            // Arrange
            var cartKey = "cart";
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            // Act
            await _shoppingCartService.DeleteItemAsync(gameKey);

            // Assert
            var usershoppingCart = await MockShoppingCartCash.Object.GetAsync(cartKey);
            Assert.Single(usershoppingCart.Items);
            MockShoppingCartCash.Verify(sc => sc.SetAsync(It.IsAny<string>(), It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemAsync_WhichShoppingCartContains_ShouldDecreaseQuantity()
        {
            // Arrange
            var cartKey = "cart";

            var gameKey = "55aa36g4-16b0-4eec-8c27-39b54e67664d";

            // Act
            await _shoppingCartService.DeleteItemAsync(gameKey);

            // Assert
            var usershoppingCart = await MockShoppingCartCash.Object.GetAsync(cartKey);
            var item = usershoppingCart.Items.SingleOrDefault(i => i.GameName == "Warcraft III");
            Assert.Equal(2, usershoppingCart.Items.Count);
            Assert.Equal(1, item.Quantity);
            MockShoppingCartCash.Verify(sc => sc.SetAsync(It.IsAny<string>(), It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnListOfShoppingCartItems()
        {
            // Act
            var items = await _shoppingCartService.GetAllItemsAsync();

            // Assert
            Assert.Equal(2, items.Count());
        }
    }
}
