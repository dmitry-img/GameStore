using System;
using System.Linq;
using System.Text;
using GameStore.BLL.Strategies.Payment;
using GameStore.BLL.UnitTests.Common;
using Xunit;

namespace GameStore.BLL.UnitTests.Strategies
{
    public class PaymentStrategyTests : BaseTest
    {
        [Fact]
        public void BankPaymentStrategy_Pay_ShouldReturnCorrectData()
        {
            // Arrange
            var strategy = new BankPaymentStrategy();
            var order = Orders.First();

            // Act
            var result = strategy.Pay(order);

            var resultString = Encoding.ASCII.GetString(result.ToArray());
            var expectedString = $"The order {order.Id} has been successully created!{Environment.NewLine}" +
                $"Total price: {order.OrderDetails.Sum(od => od.Price * od.Quantity)}";

            // Assert
            Assert.Equal(expectedString, resultString);
        }

        [Fact]
        public void IBoxPaymentStrategy_Pay_ShouldReturnOrderId()
        {
            // Arrange
            var strategy = new IBoxPaymentStrategy();
            var order = Orders.First();

            // Act
            var result = strategy.Pay(order);

            // Assert
            Assert.Equal(order.Id, result);
        }

        [Fact]
        public void VisaPaymentStrategy_Pay_ShouldReturnOrderId()
        {
            // Arrange
            var strategy = new VisaPaymentStrategy();
            var order = Orders.First();

            // Act
            var result = strategy.Pay(order);

            // Assert
            Assert.Equal(order.Id, result);
        }
    }
}
