using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class RoleServiceTests : BaseTest
    {
        private readonly RoleService _roleService;

        public RoleServiceTests()
        {
            _roleService = new RoleService(MockUow.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateRoleAndSaveChanges()
        {
            // Arrange
            var roleDto = new CreateRoleDTO { Name = "Test" };

            // Act
            await _roleService.CreateAsync(roleDto);

            // Assert
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
            MockUow.Verify(uow => uow.Roles.Create(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRoles()
        {
            // Act
            var result = await _roleService.GetAllAsync();

            // Assert
            Assert.Equal(Roles.Count, result.Count());
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ShouldReturnPaginatedRoles()
        {
            // Arrange
            var paginationDto = new PaginationDTO { PageNumber = 1, PageSize = 1 };

            // Act
            var result = await _roleService.GetAllWithPaginationAsync(paginationDto);

            // Assert
            Assert.Single(result.Items);
            Assert.Equal("Administrator", result.Items.First().Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRoleById()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _roleService.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal("Administrator", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteRole()
        {
            // Arrange
            int id = 1;

            // Act
            await _roleService.DeleteAsync(id);

            // Assert
            MockUow.Verify(uow => uow.Roles.Delete(It.IsAny<int>()), Times.Once);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }
    }
}
