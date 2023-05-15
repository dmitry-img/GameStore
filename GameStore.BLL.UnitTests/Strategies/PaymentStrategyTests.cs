using System;
using System.Threading.Tasks;
using GameStore.BLL.Strategies;
using GameStore.DAL.Entities;
using Xunit;

public class PaymentStrategyTests
{
    [Fact]
    public void BankPaymentStrategy_ShouldProcessPayment()
    {
        // Arrange
        var order = new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now };
        var strategy = new BankPaymentStrategy();

        // Act
        strategy.Pay(order);

        // Assert
    }

    [Fact]
    public void IBoxPaymentStrategy_ShouldProcessPayment()
    {
        // Arrange
        var order = new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now };
        var strategy = new IBoxPaymentStrategy();

        // Act
        strategy.Pay(order);

        // Assert
    }

    [Fact]
    public void VisaPaymentStrategy_ShouldProcessPayment()
    {
        // Arrange
        var order = new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now };
        var strategy = new VisaPaymentStrategy();

        // Act
        strategy.Pay(order);

        // Assert
    }
}
