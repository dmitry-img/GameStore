using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.Enums;
using GameStore.BLL.Factories;
using GameStore.BLL.Interfaces;
using GameStore.Shared.Infrastructure;
using Moq;
using Unity;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class PaymentsControllerTests : BaseTest
    {
        private readonly PaymentsController _controller;
        private readonly Mock<IPaymentService> _paymentServiceMock;

        public PaymentsControllerTests()
        {
            _paymentServiceMock = new Mock<IPaymentService>();
            _controller = new PaymentsController(_paymentServiceMock.Object);
        }

        [Fact]
        public async Task ProcessBankPayment_ShouldInvoke_PaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<MemoryStream>(orderId, PaymentType.Bank, UserContext.UserObjectId))
                .ReturnsAsync(new MemoryStream());

            // Act
            await _controller.ProcessBankPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(service => service.ProcessPayment<MemoryStream>(orderId, PaymentType.Bank, UserContext.UserObjectId), Times.Once);
        }

        [Fact]
        public async Task ProcessIboxPayment_ShouldInvoke_PaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<int>(orderId, PaymentType.IBox, UserContext.UserObjectId))
                .ReturnsAsync(orderId);

            // Act
            await _controller.ProcessIboxPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(
                service => service.ProcessPayment<int>(orderId, PaymentType.IBox, UserContext.UserObjectId), Times.Once);
        }

        [Fact]
        public async Task PocessVisaPayment_ShoultInvoke_PaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<int>(orderId, PaymentType.Visa, UserContext.UserObjectId))
                .ReturnsAsync(orderId);

            // Act
            await _controller.PocessVisaPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(
                service => service.ProcessPayment<int>(orderId, PaymentType.Visa, UserContext.UserObjectId), Times.Once);
        }
    }
}
