using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class RolesControllerTests
    {
        private readonly RolesController _rolesController;
        private readonly Mock<IRoleService> _roleServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<CreateRoleDTO>> _createRoleValidatorMock;
        private readonly Mock<IValidator<UpdateRoleDTO>> _updateRoleValidatorMock;

        public RolesControllerTests()
        {
            _roleServiceMock = new Mock<IRoleService>();
            _validationServiceMock = new Mock<IValidationService>();
            _createRoleValidatorMock = new Mock<IValidator<CreateRoleDTO>>();
            _updateRoleValidatorMock = new Mock<IValidator<UpdateRoleDTO>>();

            _rolesController = new RolesController(
                _roleServiceMock.Object,
                _validationServiceMock.Object,
                _createRoleValidatorMock.Object,
                _updateRoleValidatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsRoles()
        {
            // Arrange
            var roles = new List<GetRoleDTO>
            {
                new GetRoleDTO { Id = 1, Name = "Role1" },
                new GetRoleDTO { Id = 2, Name = "Role2" }
            };

            _roleServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(roles);

            // Act
            var result = await _rolesController.GetAll();

            // Assert
            Assert.IsType<JsonResult<IEnumerable<GetRoleDTO>>>(result);
        }

        [Fact]
        public async Task CreateAsync_CallsCreateAsync()
        {
            // Arrange
            var createRoleDTO = new CreateRoleDTO { Name = "Role1" };

            _roleServiceMock.Setup(x => x.CreateAsync(createRoleDTO));

            // Act
            await _rolesController.Create(createRoleDTO);

            // Assert
            _roleServiceMock.Verify(x => x.CreateAsync(createRoleDTO), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteAsync()
        {
            // Arrange
            var id = 1;

            _roleServiceMock.Setup(x => x.DeleteAsync(id));

            // Act
            await _rolesController.Delete(id);

            // Assert
            _roleServiceMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsRole()
        {
            // Arrange
            var id = 1;
            var role = new GetRoleDTO { Id = 1, Name = "Role1" };

            _roleServiceMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(role);

            // Act
            var result = await _rolesController.GetById(id);

            // Assert
            Assert.IsType<JsonResult<GetRoleDTO>>(result);
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ReturnsRoles()
        {
            // Arrange
            var roles = new List<GetRoleDTO>
            {
                new GetRoleDTO { Id = 1, Name = "Role1" },
                new GetRoleDTO { Id = 2, Name = "Role2" }
            };

            _roleServiceMock.Setup(x => x.GetAllWithPaginationAsync(It.IsAny<PaginationDTO>())).ReturnsAsync(
                PaginationResult<GetRoleDTO>.ToPaginationResult(roles, 1, 1));

            // Act
            var result = await _rolesController.GetAllWithPagination(It.IsAny<PaginationDTO>());

            // Assert
            Assert.IsType<JsonResult<PaginationResult<GetRoleDTO>>>(result);
        }
    }
}
