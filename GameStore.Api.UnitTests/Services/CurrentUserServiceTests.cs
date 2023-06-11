using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GameStore.Api.Interfaces;
using GameStore.Api.Services;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Services
{
    public class CurrentUserServiceTests
    {
        [Fact]
        public void GetCurrentUserObjectId_ReturnsUserId()
        {
            // Arrange
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "test-user-id"));

            var identityProviderMock = new Mock<IUserIdentityProvider>();
            identityProviderMock.Setup(x => x.GetIdentity()).Returns(identity);

            var currentUserService = new CurrentUserService(identityProviderMock.Object);

            // Act
            var result = currentUserService.GetCurrentUserObjectId();

            // Assert
            Assert.Equal("test-user-id", result);
        }

        [Fact]
        public void GetCurrentUserObjectId_ReturnsNullWhenNotAuthenticated()
        {
            // Arrange
            var identity = new ClaimsIdentity();

            var identityProviderMock = new Mock<IUserIdentityProvider>();
            identityProviderMock.Setup(x => x.GetIdentity()).Returns(identity);

            var currentUserService = new CurrentUserService(identityProviderMock.Object);

            // Act
            var result = currentUserService.GetCurrentUserObjectId();

            // Assert
            Assert.Null(result);
        }
    }
}
