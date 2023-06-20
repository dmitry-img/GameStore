using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.BLL.UnitTests.Mocks.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class UserServiceTests : BaseTest
    {
        private readonly UserService _userService;
        private readonly IUserValidationService _userValidationService;

        public UserServiceTests()
        {
            _userValidationService = new UserValidationService(MockUow.Object);
            _userService = new UserService(MockUow.Object, _userValidationService, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO()
            {
                Username = "TestUser",
                Password = "123123",
                ConfirmPassword = "123123"
            };

            // Act
            await _userService.CreateAsync(createUserDTO);

            // Assert
            MockUow.Verify(uow => uow.Users.Create(It.IsAny<User>()), Times.Once);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteUser()
        {
            // Act
            await _userService.DeleteAsync(RegularUserObjectId);

            // Assert
            var regularUser = Users.FirstOrDefault(u => u.ObjectId == RegularUserObjectId);
            MockUow.Verify(uow => uow.Users.Delete(It.IsAny<int>()), Times.Once);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ShouldReturnPaginatedUsers()
        {
            // Arrange
            var paginationDto = new PaginationDTO { PageNumber = 1, PageSize = 1 };

            // Act
            var result = await _userService.GetAllWithPaginationAsync(AdministratorObjectId, paginationDto);

            // Assert
            Assert.Single(result.Items);
            Assert.Equal(ManagerObjectId, result.Items.First().ObjectId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserDTO()
        {
            // Act
            var result = await _userService.GetByIdAsync(RegularUserObjectId);

            // Assert
            Assert.Equal(RegularUserObjectId, result.ObjectId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            // Arrange
            string newName = "New Name";

            var updateUserDTO = new UpdateUserDTO
            {
                Username = newName
            };

            // Act
            await _userService.UpdateAsync(RegularUserObjectId, updateUserDTO);

            // Assert
            var user = Users.FirstOrDefault(u => u.ObjectId == RegularUserObjectId);

            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
            Assert.Equal(newName, user.Username);
        }

        [Fact]
        public async Task BanAsync_ShouldBanUser()
        {
            // Arrange
            var commentId = 1;
            var banDTO = new BanDTO { CommentId = commentId, BanDuration = BanDuration.OneDay };

            // Act
            await _userService.BanAsync(banDTO);

            // Assert
            var comment = Comments.FirstOrDefault(c => c.Id == commentId);

            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
            Assert.NotNull(comment.User.BanEndDate);
        }

        [Fact]
        public async Task IsBannedAsync_ShouldReturnTrue_WhenUserIsBanned()
        {
            // Act
            bool isBanned = await _userService.IsBannedAsync(BannedRegularUserObjectId);

            // Assert
            Assert.True(isBanned);
        }

        [Fact]
        public async Task IsBannedAsync_ShouldReturnFalse_WhenUserIsNotBanned()
        {
            // Act
            bool isBanned = await _userService.IsBannedAsync(RegularUserObjectId);

            // Assert
            Assert.False(isBanned);
        }
    }
}
