using System.IO;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Enums;
using GameStore.BLL.Services;
using GameStore.BLL.Strategies.Payment;
using GameStore.BLL.UnitTests.Common;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class PaymentServiceTests : BaseTest
    {
        private const string TestObjectId = "TestObjectId";
        private readonly PaymentService _paymentService;
        private readonly int _orderId = 1;

        public PaymentServiceTests()
        {
            _paymentService = new PaymentService(
                MockUow.Object,
                MockPaymentStrategyFactory.Object,
                MockShoppingCartCash.Object,
                MockLogger.Object);
        }

        [Fact]
        public async Task ProcessPayment_ShouldReturnMemoryStream_WhenPaymentTypeIsBank()
        {
            // Arrange
            MockPaymentStrategyFactory.Setup(f => f.GetStrategy<MemoryStream>(PaymentType.Bank)).Returns(new BankPaymentStrategy());

            // Act
            var result = await _paymentService.ProcessPayment<MemoryStream>(_orderId, PaymentType.Bank, TestObjectId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MemoryStream>(result);
        }

        [Fact]
        public async Task ProcessPayment_ShouldReturnOrderId_WhenPaymentTypeIsIBox()
        {
            // Arrange
            MockPaymentStrategyFactory.Setup(f => f.GetStrategy<int>(PaymentType.IBox)).Returns(new IBoxPaymentStrategy());

            // Act
            var result = await _paymentService.ProcessPayment<int>(_orderId, PaymentType.IBox, TestObjectId);

            // Assert
            Assert.Equal(_orderId, result);
        }

        [Fact]
        public async Task ProcessPayment_ShouldReturnOrderId_WhenPaymentTypeIsVisa()
        {
            // Arrange
            MockPaymentStrategyFactory.Setup(f => f.GetStrategy<int>(PaymentType.Visa)).Returns(new VisaPaymentStrategy());

            // Act
            var result = await _paymentService.ProcessPayment<int>(_orderId, PaymentType.Visa, TestObjectId);

            // Assert
            Assert.Equal(_orderId, result);
        }
    }
}
