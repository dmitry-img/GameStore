using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class ShoppingCartsControllerTests
    {
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock;
        private readonly ShoppingCartsController _shoppingCartsController;

        public ShoppingCartsControllerTests()
        {
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _shoppingCartsController = new ShoppingCartsController(_shoppingCartServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldInvoke_GetAllItemsAsync_And_ReturnJson()
        {
            // Arrange
            var items = new List<GetShoppingCartItemDTO>
            {
                new GetShoppingCartItemDTO(),
                new GetShoppingCartItemDTO()
            };

            _shoppingCartServiceMock.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(items);

            // Act
            var result = await _shoppingCartsController.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<GetShoppingCartItemDTO>>>(result);
            _shoppingCartServiceMock.Verify(s => s.GetAllItemsAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var newItem = new CreateShoppingCartItemDTO();

            // Act
            var result = await _shoppingCartsController.AddItem(newItem);

            // Assert
            _shoppingCartServiceMock.Verify(s => s.AddItemAsync(It.IsAny<CreateShoppingCartItemDTO>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItem_ShouldInvoke_DeleteItemAsync()
        {
            // Arrange
            var gameKey = "test-key";

            // Act
            var result = await _shoppingCartsController.DeleteItem(gameKey);

            // Assert
            _shoppingCartServiceMock.Verify(s => s.DeleteItemAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
