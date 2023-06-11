using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class UsersControllerTests
    {
        private const string TestObjectId = "TestObjectId";
        private readonly UsersController _usersControllerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<CreateUserDTO>> _createUserValidatorMock;
        private readonly Mock<IValidator<UpdateUserDTO>> _updateUserValidatorMock;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _validationServiceMock = new Mock<IValidationService>();
            _createUserValidatorMock = new Mock<IValidator<CreateUserDTO>>();
            _updateUserValidatorMock = new Mock<IValidator<UpdateUserDTO>>();

            _usersControllerMock = new UsersController(
                _userServiceMock.Object,
                _currentUserServiceMock.Object,
                _validationServiceMock.Object,
                _createUserValidatorMock.Object,
                _updateUserValidatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsUsers()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var users = new List<GetUserDTO>
            {
                new GetUserDTO(),
                new GetUserDTO()
            };

            _userServiceMock.Setup(x => x.GetAllWithPaginationAsync(TestObjectId, pagination)).ReturnsAsync(
                PaginationResult<GetUserDTO>.ToPaginationResult(users, 1, 1));

            // Act
            var result = await _usersControllerMock.GetAllWithPagination(pagination);

            // Assert
            Assert.IsType<JsonResult<PaginationResult<GetUserDTO>>>(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO
            {
                Email = "User1",
                Password = "123456",
                ConfirmPassword = "123456"
            };

            _createUserValidatorMock.Setup(x => x.ValidateAsync(createUserDTO, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _usersControllerMock.Create(createUserDTO);

            // Assert
            _userServiceMock.Verify(x => x.CreateAsync(createUserDTO), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsUpdateAsync()
        {
            // Arrange
            var updateUserDTO = new UpdateUserDTO();

            _updateUserValidatorMock.Setup(x => x.ValidateAsync(updateUserDTO, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _usersControllerMock.Update(TestObjectId, updateUserDTO);

            // Assert
            _userServiceMock.Verify(x => x.UpdateAsync(TestObjectId, updateUserDTO), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsync()
        {
            // Act
            var result = await _usersControllerMock.Delete(TestObjectId);

            // Assert
            _userServiceMock.Verify(x => x.DeleteAsync(TestObjectId), Times.Once);
        }
    }
}
