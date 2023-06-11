using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class AuthServiceTests : BaseTest
    {
        private readonly AuthService _authService;
        private readonly IUserValidationService _userValidationService;

        public AuthServiceTests()
        {
            _userValidationService = new UserValidationService(MockUow.Object);
            _authService = new AuthService(
                MockUow.Object,
                _userValidationService,
                MockLogger.Object,
                MockConfigurationWrapper.Object);
        }

        [Fact]
        public async Task RegisterAsync_ValidRegistration_CreatesUser()
        {
            // Arrange
            var registrationDTO = new RegistrationDTO
            {
                Username = "testuser",
                Email = "test@test.com",
                Password = "Test@123"
            };

            // Act
            await _authService.RegisterAsync(registrationDTO);

            // Assert
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ValidLogin_ReturnsAuthToken()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Username = "regularUser",
                Password = "q1w2e3r4"
            };

            // Act
            var result = await _authService.LoginAsync(loginDTO);

            // Assert
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task LogoutAsync_ValidUserObjectId_UserLoggedOut()
        {
            // Act
            await _authService.LogoutAsync(RegularUserObjectId);

            // Assert
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task RefreshAsync_ValidRefreshToken_ReturnsNewAuthToken()
        {
            // Arrange
            var refreshToken = "9027deea-13f6-4d04-9275-b2af7420bdbd";

            // Act
            var result = await _authService.RefreshAsync(refreshToken);

            // Assert
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.RefreshToken);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}
