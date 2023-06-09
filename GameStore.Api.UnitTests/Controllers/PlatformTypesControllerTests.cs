﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class PlatformTypesControllerTests : BaseTest
    {
        private readonly PlatformTypesController _platformTypesController;
        private readonly Mock<IPlatformTypeService> _platformTypeServiceMock;

        public PlatformTypesControllerTests()
        {
            _platformTypeServiceMock = new Mock<IPlatformTypeService>();

            _platformTypesController = new PlatformTypesController(_platformTypeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsPlatformTypes()
        {
            // Arrange
            var platformTypes = new List<GetPlatformTypeDTO>
            {
                new GetPlatformTypeDTO { Id = 1, Type = "PlatformType1" },
                new GetPlatformTypeDTO { Id = 2, Type = "PlatformType2" }
            };

            _platformTypeServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(platformTypes);

            // Act
            var result = await _platformTypesController.GetAll();

            // Assert
            Assert.IsType<JsonResult<IEnumerable<GetPlatformTypeDTO>>>(result);
        }
    }
}
