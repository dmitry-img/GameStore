using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class OrderServiceTests : BaseTest
    {
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _orderService = new OrderService(MockUow.Object, MockShoppingCartCash.Object, Mapper);
        }

        [Fact]
        public async Task Create_ShouldReturnOrderDTO_WhenCartNotEmpty()
        {
            // Arrange
            var customerId = 1;

            MockUow.Setup(u => u.Orders.Create(It.IsAny<Order>()))
                .Callback((Order order) =>
                {
                    Orders.Add(order);
                    order.Id = Orders.Count;
                });

            MockUow.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.Create(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetOrderDTO>(result);
            Assert.Equal(Orders.Last().Id, result.OrderId);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal(Orders.Last().OrderDetails.Sum(od => od.Price), result.TotalSum);
        }
    }
}
