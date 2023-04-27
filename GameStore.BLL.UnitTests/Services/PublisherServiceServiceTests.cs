using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PublisherServiceServiceTests : BaseTest
    {
        private readonly PublisherService _publisherService;

        public PublisherServiceServiceTests()
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
    }
}
