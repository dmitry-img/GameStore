using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Validators.Order;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly IValidationService _validationService;
        private readonly IValidator<UpdateOrderDTO> _updateOrderValidator;
        private readonly OrdersController _ordersController;

        public OrdersControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockCurrentUserService = new Mock<ICurrentUserService>();
            _validationService = new ValidationService();
            _updateOrderValidator = new UpdateOrderDTOValidator();

            _ordersController = new OrdersController(
                    _mockOrderService.Object,
                    _mockCurrentUserService.Object,
                    _validationService,
                    _updateOrderValidator);
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            // Arrange
            var userObjectId = "123";
            _mockCurrentUserService.Setup(s => s.GetCurrentUserObjectId()).Returns(userObjectId);
            _mockOrderService.Setup(s => s.CreateAsync(userObjectId)).ReturnsAsync(new GetOrderDTO());

            // Act
            var result = await _ordersController.Create();

            // Assert
            var okResult = result as OkNegotiatedContentResult<GetOrderDTO>;
            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task GetAllWithPagination_ReturnsJsonResult()
        {
            // Arrange
            var paginationDto = new PaginationDTO() { PageNumber = 1, PageSize = 1 };
            var orderList = new List<GetOrderDTO> { new GetOrderDTO() };
            _mockOrderService.Setup(s => s.GetAllWithPaginationAsync(paginationDto))
                .ReturnsAsync(PaginationResult<GetOrderDTO>.ToPaginationResult(orderList, paginationDto.PageNumber, paginationDto.PageSize));

            // Act
            var result = await _ordersController.GetAllWithPagination(paginationDto);

            // Assert
            var okResult = result as JsonResult<PaginationResult<GetOrderDTO>>;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Content);
            Assert.Equal(orderList, okResult.Content.Items);
        }

        [Fact]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var orderId = 1;
            var updateOrderDTO = new UpdateOrderDTO();

            // Act
            var result = await _ordersController.Update(orderId, updateOrderDTO);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockOrderService.Verify(s => s.UpdateAsync(orderId, updateOrderDTO), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsJsonResult()
        {
            // Arrange
            var orderId = 1;
            var orderDto = new GetOrderDTO();
            _mockOrderService.Setup(s => s.GetAsync(orderId)).ReturnsAsync(orderDto);

            // Act
            var result = await _ordersController.GetById(orderId);

            // Assert
            var okResult = result as JsonResult<GetOrderDTO>;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Content);
        }
    }
}
