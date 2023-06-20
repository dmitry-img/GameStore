using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class PublisherServiceTests : BaseTest
    {
        private readonly PublisherService _publisherService;

        public PublisherServiceTests()
        {
            _publisherService = new PublisherService(MockUow.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task GetAllBriefAsync_ShouldReturnListOfGetGameDTOs()
        {
            // Act
            var result = await _publisherService.GetAllBriefAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<IEnumerable<GetPublisherBriefDTO>>(result);
        }

        [Fact]
        public async Task GetByCompanyNameAsync_ShouldReturnPublisherDTOs()
        {
            // Arrange
            var gameKey = "Rockstar";

            // Act
            var result = await _publisherService.GetByCompanyNameAsync(gameKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Rockstar", result.CompanyName);
            Assert.Equal("Rockstar description...", result.Description);
            Assert.Equal("https://www.rockstargames.com/", result.HomePage);
        }

        [Fact]
        public async Task GetByCompanyNameAsync_PublisherNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var companyName = "test";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _publisherService.GetByCompanyNameAsync(companyName));
        }

        [Fact]
        public async Task CreateAsync_WithValidPublisherDTO_CreatesPublisher()
        {
            // Arrange
            var publisherDTO = new CreatePublisherDTO
            {
                CompanyName = "Test Publisher",
                Description = "Test Description",
                HomePage = "https://www.my-test-publisher.com/"
            };

            MockUow.Setup(r => r.Publishers.Create(It.IsAny<Publisher>())).Callback((Publisher publisher) =>
            {
                publisher.Id = Publishers.Count + 1;
                Publishers.Add(publisher);
            });

            // Act
            await _publisherService.CreateAsync(publisherDTO);

            // Assert
            var allPublishers = await MockUow.Object.Publishers.GetAllAsync();
            Assert.Equal(4, allPublishers.Count());
            MockUow.Verify(uow => uow.Publishers.Create(It.IsAny<Publisher>()), Times.Once);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_PublisherExists_DeletesPublisher()
        {
            // Arrange
            var id = 1;

            // Act
            await _publisherService.DeleteAsync(id);

            // Assert
            var order = Orders.FirstOrDefault(o => o.Id == id);

            Assert.False(order.IsDeleted);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllBriefWithPaginationAsync_PublishersExist_ReturnsPaginatedResult()
        {
            // Arrange
            var paginationDTO = new PaginationDTO
            {
                PageNumber = 1,
                PageSize = 2
            };

            // Act
            var result = await _publisherService.GetAllBriefWithPaginationAsync(paginationDTO);

            // Assert
            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public async Task UpdateAsync_ValidCompanyName_UpdatesPublisher()
        {
            // Arrange
            var companyName = "Blizzard Entertainment";
            var updatePublisherDTO = new UpdatePublisherDTO
            {
                CompanyName = companyName,
                Description = "Updated description",
                Username = "blizzard"
            };

            // Act
            await _publisherService.UpdateAsync(companyName, updatePublisherDTO);

            // Assert
            var publisher = Publishers.FirstOrDefault(p => p.CompanyName == companyName);
            Assert.Equal("Updated description", publisher.Description);
        }

        [Fact]
        public async Task IsUserAssociatedWithPublisherAsync_ValidInputs_ReturnsTrue()
        {
            // Arrange
            var userObjectId = "b53ee89f-23c3-4913-9cd5-a76dabea98b4";
            var companyName = "Blizzard Entertainment";

            // Act
            var result = await _publisherService.IsUserAssociatedWithPublisherAsync(userObjectId, companyName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsGameAssociatedWithPublisherAsync_ValidInputs_ReturnsTrue()
        {
            // Arrange
            var userObjectId = "b53ee89f-23c3-4913-9cd5-a76dabea98b4";
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            // Act
            var result = await _publisherService.IsGameAssociatedWithPublisherAsync(userObjectId, gameKey);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetCurrentCompanyNameAsync_ValidUserObjectId_ReturnsCompanyName()
        {
            // Arrange
            var userObjectId = "b53ee89f-23c3-4913-9cd5-a76dabea98b4";

            // Act
            var result = await _publisherService.GetCurrentCompanyNameAsync(userObjectId);

            // Assert
            Assert.Equal("Blizzard Entertainment", result);
        }

        [Fact]
        public async Task GetFreePublisherUsernamesAsync_PublishersExist_ReturnsUsernames()
        {
            // Act
            var result = await _publisherService.GetFreePublisherUsernamesAsync();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
