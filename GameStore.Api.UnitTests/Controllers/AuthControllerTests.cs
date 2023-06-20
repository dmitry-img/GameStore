using System.Threading.Tasks;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.Interfaces;
using GameStore.Shared.Infrastructure;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class AuthControllerTests : BaseTest
    {
        private readonly AuthController _authControoller;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<RegistrationDTO>> _registrationValidatorMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _validationServiceMock = new Mock<IValidationService>();
            _registrationValidatorMock = new Mock<IValidator<RegistrationDTO>>();

            _authControoller = new AuthController(
                _authServiceMock.Object,
                _validationServiceMock.Object,
                _registrationValidatorMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldInvoke_RegisterAsync()
        {
            // Arrange
            var registrationDTO = new RegistrationDTO
            {
                Email = "test-email",
                Password = "test-password"
            };

            // Act
            var result = await _authControoller.Register(registrationDTO);

            // Assert
            _authServiceMock.Verify(x => x.RegisterAsync(registrationDTO), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldInvoke_LoginAsync()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Username = "test-username",
                Password = "test-password"
            };

            // Act
            var result = await _authControoller.Login(loginDTO);

            // Assert
            _authServiceMock.Verify(x => x.LoginAsync(loginDTO), Times.Once);
        }

        [Fact]
        public async Task LogoutAsync_ShouldInvoke_LogoutAsync()
        {
            // Act
            var result = await _authControoller.Logout(UserContext.UserObjectId);

            // Assert
            _authServiceMock.Verify(x => x.LogoutAsync(UserContext.UserObjectId), Times.Once);
        }

        [Fact]
        public async Task RefreshAsync_ShouldInvoke_RefreshAsync()
        {
            // Arrange
            var testRefreshToken = "testRefreshToken";

            // Act
            var result = await _authControoller.Refresh(testRefreshToken);

            // Assert
            _authServiceMock.Verify(x => x.RefreshAsync(testRefreshToken), Times.Once);
        }
    }
}
