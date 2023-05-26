using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Validators;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class PublishersControllerTests
    {
        private readonly Mock<IPublisherService> _publisherServiceMock;
        private readonly IValidationService _validationService;
        private readonly PublishersController _publishersController;
        private readonly IValidator<CreatePublisherDTO> _createPublisherValidator;

        public PublishersControllerTests()
        {
            _publisherServiceMock = new Mock<IPublisherService>();
            _validationService = new ValidationService();
            _createPublisherValidator = new CreatePublisherDTOValidator();
            _publishersController = new PublishersController(
                _publisherServiceMock.Object,
                _validationService,
                _createPublisherValidator);
        }

        [Fact]
        public async Task GetAll_ShouldInvoke_GetByCompanyNameAsync_And_ReturnJson()
        {
            // Arrange
            var comapnyName = "Test";

            var expectedPublisher = new GetPublisherDTO()
            {
                CompanyName = comapnyName,
            };

            _publisherServiceMock.Setup(x => x.GetByCompanyNameAsync(It.IsAny<string>())).ReturnsAsync(expectedPublisher);

            // Act
            var result = await _publishersController.GetByCompanyName(comapnyName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<GetPublisherDTO>>(result);
            _publisherServiceMock.Verify(s => s.GetByCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var newPublisher = new CreatePublisherDTO()
            {
                CompanyName = "Test",
                Description = "Test Description",
                HomePage = "https://test.com/"
            };

            // Act
            var result = await _publishersController.Create(newPublisher);

            // Assert
            _publisherServiceMock.Verify(s => s.CreateAsync(It.IsAny<CreatePublisherDTO>()), Times.Once);
        }

        [Fact]
        public async Task GetAllBrief_ShouldInvoke_GetAllBriefAsync_And_ReturnJson()
        {
            // Arrange
            var publishers = new List<GetPublisherBriefDTO>
            {
                new GetPublisherBriefDTO(),
                new GetPublisherBriefDTO()
            };

            _publisherServiceMock.Setup(x => x.GetAllBriefAsync()).ReturnsAsync(publishers);

            // Act
            var result = await _publishersController.GetAllBrief();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<GetPublisherBriefDTO>>>(result);
            _publisherServiceMock.Verify(s => s.GetAllBriefAsync(), Times.Once);
        }
    }
}
