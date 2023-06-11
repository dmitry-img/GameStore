using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class ValidationServiceTests : BaseTest
    {
        private readonly ValidationService _validationService;
        private readonly Mock<IValidator<string>> _mockValidator;

        public ValidationServiceTests()
        {
            _mockValidator = new Mock<IValidator<string>>();
            _validationService = new ValidationService();
        }

        [Fact]
        public void Validate_WhenValidationFails_ThrowsValidationException()
        {
            // Arrange
            var failure = new ValidationFailure("Property", "Error message");
            var validationResult = new ValidationResult(new List<ValidationFailure> { failure });

            _mockValidator.Setup(v => v.Validate(It.IsAny<string>())).Returns(validationResult);

            // Act and Assert
            var ex = Assert.Throws<BadRequestException>(() => _validationService.Validate("TestDto", _mockValidator.Object));
            Assert.Contains("Error message", ex.Message);
        }
    }
}
