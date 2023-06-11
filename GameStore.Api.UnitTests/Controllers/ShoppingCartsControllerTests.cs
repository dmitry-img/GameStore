using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Validators.ShoppingCart;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class ShoppingCartsControllerTests
    {
        private readonly ShoppingCartsController _shoppingCartsController;
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateShoppingCartItemDTO> _createPublisherValidator;

        public ShoppingCartsControllerTests()
        {
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _validationService = new ValidationService();
            _createPublisherValidator = new CreateShoppingCartItemDTOValidator();
            _shoppingCartsController = new ShoppingCartsController(
                _shoppingCartServiceMock.Object,
                _currentUserServiceMock.Object,
                _validationService,
                _createPublisherValidator);
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

            _shoppingCartServiceMock.Setup(x => x.GetAllItemsAsync(It.IsAny<string>())).ReturnsAsync(items);

            // Act
            var result = await _shoppingCartsController.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<GetShoppingCartItemDTO>>>(result);
            _shoppingCartServiceMock.Verify(s => s.GetAllItemsAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var newItem = new CreateShoppingCartItemDTO()
            {
                GameKey = "TestGameKey",
                GameName = "TestGame",
                GamePrice = 1,
                Quantity = 1
            };

            // Act
            var result = await _shoppingCartsController.AddItem(newItem);

            // Assert
            _shoppingCartServiceMock.Verify(
                s => s.AddItemAsync(
                    It.IsAny<string>(),
                    It.IsAny<CreateShoppingCartItemDTO>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItem_ShouldInvoke_DeleteItemAsync()
        {
            // Arrange
            var gameKey = "test-key";

            // Act
            var result = await _shoppingCartsController.DeleteItem(gameKey);

            // Assert
            _shoppingCartServiceMock.Verify(s => s.DeleteItemAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetGameQuantity_ShouldInvoke_GetGameQuantityByKeyAsync_And_ReturnOk()
        {
            // Arrange
            var gameKey = "test-key";
            var quantity = 1;

            _shoppingCartServiceMock.Setup(x => x.GetGameQuantityByKeyAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(quantity);

            // Act
            var result = await _shoppingCartsController.GetGameQuantity(gameKey);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<int>>(result);
            _shoppingCartServiceMock.Verify(s => s.GetGameQuantityByKeyAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
