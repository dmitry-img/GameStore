using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GameStore.Api.Controllers;
using GameStore.BLL.Enums;
using GameStore.BLL.Factories;
using GameStore.BLL.Interfaces;
using Moq;
using Unity;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class PaymentsControllerTests
    {
        private readonly PaymentsController _controller;
        private readonly Mock<IPaymentService> _paymentServiceMock;
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;
        private readonly Mock<IUnityContainer> _unitContainerMock;

        public PaymentsControllerTests()
        {
            _paymentServiceMock = new Mock<IPaymentService>();
            _unitContainerMock = new Mock<IUnityContainer>();
            _paymentStrategyFactory = new PaymentStrategyFactory(_unitContainerMock.Object);
            _controller = new PaymentsController(_paymentServiceMock.Object, _paymentStrategyFactory);
        }

        [Fact]
        public async Task ProcessBankPayment_CallsPaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<MemoryStream>(orderId, PaymentType.Bank))
                .ReturnsAsync(new MemoryStream());

            // Act
            await _controller.ProcessBankPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(service => service.ProcessPayment<MemoryStream>(orderId, PaymentType.Bank), Times.Once);
        }

        [Fact]
        public async Task ProcessIboxPayment_CallsPaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<int>(orderId, PaymentType.IBox))
                .ReturnsAsync(orderId);

            // Act
            await _controller.ProcessIboxPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(service => service.ProcessPayment<int>(orderId, PaymentType.IBox), Times.Once);
        }

        [Fact]
        public async Task PocessVisaPayment_CallsPaymentServiceWithCorrectParameters()
        {
            // Arrange
            var orderId = 1;
            _paymentServiceMock
                .Setup(service => service.ProcessPayment<int>(orderId, PaymentType.Visa))
                .ReturnsAsync(orderId);

            // Act
            await _controller.PocessVisaPayment(orderId);

            // Assert
            _paymentServiceMock.Verify(service => service.ProcessPayment<int>(orderId, PaymentType.Visa), Times.Once);
        }
    }
}
