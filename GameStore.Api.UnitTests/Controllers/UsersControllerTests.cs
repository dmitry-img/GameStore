using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Interfaces;
using GameStore.Shared.Infrastructure;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class UsersControllerTests : BaseTest
    {
        private readonly UsersController _usersControllerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<CreateUserDTO>> _createUserValidatorMock;
        private readonly Mock<IValidator<UpdateUserDTO>> _updateUserValidatorMock;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _validationServiceMock = new Mock<IValidationService>();
            _createUserValidatorMock = new Mock<IValidator<CreateUserDTO>>();
            _updateUserValidatorMock = new Mock<IValidator<UpdateUserDTO>>();

            _usersControllerMock = new UsersController(
                _userServiceMock.Object,
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

            _userServiceMock.Setup(x => x.GetAllWithPaginationAsync(UserContext.UserObjectId, pagination)).ReturnsAsync(
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
            var result = await _usersControllerMock.Update(UserContext.UserObjectId, updateUserDTO);

            // Assert
            _userServiceMock.Verify(x => x.UpdateAsync(UserContext.UserObjectId, updateUserDTO), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsync()
        {
            // Act
            var result = await _usersControllerMock.Delete(UserContext.UserObjectId);

            // Assert
            _userServiceMock.Verify(x => x.DeleteAsync(UserContext.UserObjectId), Times.Once);
        }
    }
}
