using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Enums;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class OrderServiceTests : BaseTest
    {
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderService = new OrderService(MockUow.Object, MockShoppingCartCash.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnOrderDTO_WhenCartNotEmpty()
        {
            // Arrange
            MockUow.Setup(u => u.Orders.Create(It.IsAny<Order>()))
                .Callback((Order order) =>
                {
                    Orders.Add(order);
                    order.Id = Orders.Count;
                });

            MockUow.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.CreateAsync(RegularUserObjectId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetOrderDTO>(result);
            Assert.Equal(Orders.Last().Id, result.Id);
            Assert.Equal(RegularUserObjectId, result.CustomerId);
            Assert.Equal(Orders.Last().OrderDetails.Sum(od => od.Price), result.TotalSum);
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ValidPaginationDTO_ReturnsOrders()
        {
            // Arrange
            var paginationDTO = new PaginationDTO
            {
                PageNumber = 1,
                PageSize = 1
            };

            // Act
            var result = await _orderService.GetAllWithPaginationAsync(paginationDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetAsync_ValidOrderId_ReturnsOrder()
        {
            // Arrange
            var orderId = 1;

            // Act
            var result = await _orderService.GetAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task GetAsync_InvalidOrderId_ThrowsNotFoundException()
        {
            // Arrange
            var orderId = 1000;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _orderService.GetAsync(orderId));
        }

        [Fact]
        public async Task UpdateAsync_ValidOrderIdAndOrderDTO_UpdatesOrder()
        {
            // Arrange
            var orderId = 1;
            var updateOrderDTO = new UpdateOrderDTO
            {
                OrderState = OrderState.Shipped,
                OrderDetails = new List<UpdateOrderDetailDTO>()
                {
                   new UpdateOrderDetailDTO
                   {
                       GameKey = "55aa36g4-16b0-4eec-8c27-39b54e67664d",
                       Quantity = 1
                   },
                   new UpdateOrderDetailDTO
                   {
                       GameKey = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
                       Quantity = 2
                   }
                }
            };

            // Act
            await _orderService.UpdateAsync(orderId, updateOrderDTO);

            // Assert
            var order = Orders.FirstOrDefault(o => o.Id == 1);
            var orderDetail1 = order.OrderDetails.FirstOrDefault(od => od.Game.Key == "55aa36g4-16b0-4eec-8c27-39b54e67664d");
            var orderDetail2 = order.OrderDetails.FirstOrDefault(od => od.Game.Key == "78cc36g4-16b0-4eec-8c27-39b54e67664d");

            Assert.Equal(OrderState.Shipped, order.OrderState);
            Assert.Equal(1, orderDetail1.Quantity);
            Assert.Equal(2, orderDetail2.Quantity);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidOrderId_ThrowsNotFoundException()
        {
            // Arrange
            var orderId = 1000;
            var updateOrderDTO = new UpdateOrderDTO();

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _orderService.UpdateAsync(orderId, updateOrderDTO));
        }

        [Fact]
        public async Task UpdateAsync_ValidOrderIdAndInvalidOrderDTO_ThrowsBadRequestException()
        {
            // Arrange
            var orderId = 1;
            var updateOrderDTO = new UpdateOrderDTO
            {
                OrderState = OrderState.Shipped,
                OrderDetails = new List<UpdateOrderDetailDTO>()
                {
                   new UpdateOrderDetailDTO
                   {
                       GameKey = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
                       Quantity = 10000
                   }
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                _orderService.UpdateAsync(orderId, updateOrderDTO));
        }
    }
}
