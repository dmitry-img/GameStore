using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Validators.Publisher;
using GameStore.Shared.Infrastructure;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class PublishersControllerTests : BaseTest
    {
        private readonly Mock<IPublisherService> _publisherServiceMock;
        private readonly IValidationService _validationService;
        private readonly PublishersController _publishersController;
        private readonly IValidator<CreatePublisherDTO> _createPublisherValidator;
        private readonly IValidator<UpdatePublisherDTO> _updatePublisherValidator;

        public PublishersControllerTests()
        {
            _publisherServiceMock = new Mock<IPublisherService>();
            _validationService = new ValidationService();
            _createPublisherValidator = new CreatePublisherDTOValidator();
            _updatePublisherValidator = new UpdatePublisherDTOValidator();
            _publishersController = new PublishersController(
                _publisherServiceMock.Object,
                _validationService,
                _createPublisherValidator,
                _updatePublisherValidator);
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

        [Fact]
        public async Task GetAllBriefWithPagination_ShouldInvoke_GetAllBriefWithPaginationAsync_And_ReturnJson()
        {
            // Arrange
            var publishers = new List<GetPublisherBriefDTO>
            {
                new GetPublisherBriefDTO(),
                new GetPublisherBriefDTO()
            };

            var pagination = new PaginationDTO()
            {
                PageNumber = 1,
                PageSize = 1
            };

            _publisherServiceMock.Setup(x => x.GetAllBriefWithPaginationAsync(It.IsAny<PaginationDTO>())).ReturnsAsync(
                PaginationResult<GetPublisherBriefDTO>.ToPaginationResult(publishers, 1, 1));

            // Act
            var result = await _publishersController.GetAllBriefWithPagination(pagination);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<PaginationResult<GetPublisherBriefDTO>>>(result);
            _publisherServiceMock.Verify(s => s.GetAllBriefWithPaginationAsync(It.IsAny<PaginationDTO>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldInvoke_UpdateAsync()
        {
            // Arrange
            var updatePublisher = new UpdatePublisherDTO()
            {
                CompanyName = "Test",
                Description = "Test Description",
                HomePage = "https://test.com/"
            };

            // Act
            var result = await _publishersController.Update(UserContext.UserObjectId, updatePublisher);

            // Assert
            _publisherServiceMock.Verify(s => s.UpdateAsync(It.IsAny<string>(), It.IsAny<UpdatePublisherDTO>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldInvoke_DeleteAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _publishersController.Delete(id);

            // Assert
            _publisherServiceMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task IsUserAssiciatedWithPublisher_ShouldInvoke_IsUserAssiciatedWithPublisherAsync_And_ReturnJson()
        {
            // Arrange
            var testCompanyName = "Test";

            _publisherServiceMock.Setup(x => x.IsUserAssociatedWithPublisherAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _publishersController.IsUserAssociatedWithPublisher(testCompanyName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<bool>>(result);
            _publisherServiceMock.Verify(s => s.IsUserAssociatedWithPublisherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task IsGameAssiciatedWithPublisher_ShouldInvoke_IsGameAssiciatedWithPublisherAsync_And_ReturnJson()
        {
            // Arrange
            var testCompanyName = "Test";

            _publisherServiceMock.Setup(x => x.IsGameAssociatedWithPublisherAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _publishersController.IsGameAssociatedWithPublisher(testCompanyName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<bool>>(result);
            _publisherServiceMock.Verify(s => s.IsGameAssociatedWithPublisherAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetCurrentPublisherCompanyName_ShouldInvoke_GetCurrentPublisherCompanyNameAsync_And_ReturnJson()
        {
            // Arrange
            var companyName = "Test";

            _publisherServiceMock.Setup(x => x.GetCurrentCompanyNameAsync(It.IsAny<string>())).ReturnsAsync(companyName);

            // Act
            var result = await _publishersController.GetCurrentPublisherCompanyName();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkNegotiatedContentResult<string>>(result);
            _publisherServiceMock.Verify(s => s.GetCurrentCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetFreePublisherUsernames_ShouldInvoke_GetFreePublisherUsernamesAsync_And_ReturnJson()
        {
            // Arrange
            var usernames = new List<string>
            {
                "Test",
                "Test2"
            };

            _publisherServiceMock.Setup(x => x.GetFreePublisherUsernamesAsync()).ReturnsAsync(usernames);

            // Act
            var result = await _publishersController.GetFreePublisherUsernames();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<string>>>(result);
            _publisherServiceMock.Verify(s => s.GetFreePublisherUsernamesAsync(), Times.Once);
        }
    }
}
