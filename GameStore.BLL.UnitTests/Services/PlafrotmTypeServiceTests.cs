using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class PlafrotmTypeServiceTests : BaseTest
    {
        private readonly PlatformTypeService _platformTypeService;

        public PlafrotmTypeServiceTests()
        {
            _platformTypeService = new PlatformTypeService(MockUow.Object, Mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPlatformTypes()
        {
            // Act
            var result = await _platformTypeService.GetAllAsync();

            // Assert
            Assert.Equal(PlatformTypes.Count, result.Count());
        }
    }
}
