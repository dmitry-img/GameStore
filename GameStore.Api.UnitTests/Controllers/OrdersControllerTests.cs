using System.Threading.Tasks;
using System.Web.Http.Results;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly Mock<IOrderService> _orderServiceMock;

        public OrdersControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new OrdersController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task Create_ShouldInvoke_OrderService_WithCorrectParametersAndReturnOrder()
        {
            // Arrange
            var customerId = 1;
            var expectedOrder = new GetOrderDTO
            {
                OrderId = 1,
                CustomerId = customerId,
                TotalSum = 100
            };

            _orderServiceMock
                .Setup(service => service.Create(customerId))
                .ReturnsAsync(expectedOrder);

            // Act
            var response = await _controller.Create();
            var result = response as OkNegotiatedContentResult<GetOrderDTO>;

            // Assert
            _orderServiceMock.Verify(service => service.Create(customerId), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(expectedOrder, result.Content);
        }
    }
}
