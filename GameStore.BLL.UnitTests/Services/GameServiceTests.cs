using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Mocks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly IMapper _mapper;
        private readonly Mock<ILog> _loggerMock;
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _mockUow = MockUnitOfWork.Get();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _loggerMock = new Mock<ILog>();

            _gameService = new GameService(_mockUow.Object, _mapper, _loggerMock.Object);
        }


        [Fact]
        public async Task CreateAsync_ValidGameDTO_CreatesGame()
        {
            // Arrange
            var gameDTO = new CreateGameDTO
            {
                Name = "Test Game",
                Description = "Test Description",
                GenreIds = new List<int> { 1, 2 },
                PlatformTypeIds = new List<int> { 1, 2 },
            };

            // Act
            await _gameService.CreateAsync(gameDTO);

            // Assert
            Assert.Equal(2, _mockUow.Object.Games.GetAll().Count());
            Assert.Equal(2, _mockUow.Object.Games.Get(2).Genres.Count);
            Assert.Equal(2, _mockUow.Object.Games.Get(2).PlatformTypes.Count);
        }

        [Fact]
        public async Task UpdateAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";
            var gameDTO = new UpdateGameDTO
            {
                Name = "Updated Test Game",
                Description = "Updated Test Description",
                GenreIds = new List<int> { 1, 2 },
                PlatformTypeIds = new List<int> { 1, 2 }
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => 
                _gameService.UpdateAsync(key, gameDTO));
        }

        [Fact]
        public async Task UpdateAsync_ValidUpdateGameDTO_UpdatesGame()
        {
            // Arrange
            var key = "69bb25f3-16b0-4eec-8c27-39b54e67664d";
            var gameDTO = new UpdateGameDTO
            {
                Name = "Updated Test Game",
                Description = "Updated Test Description",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 }
            };

            // Act
            await _gameService.UpdateAsync(key, gameDTO);

            //Assert
            Assert.Equal("Updated Test Game", _mockUow.Object.Games.Get(1).Name);
            Assert.Equal("Updated Test Description", _mockUow.Object.Games.Get(1).Description);
            Assert.Equal(1, _mockUow.Object.Games.Get(1).Genres.Count);
            Assert.Equal(1, _mockUow.Object.Games.Get(1).PlatformTypes.Count);
        }

        [Fact]
        public async Task DeleteAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.DeleteAsync(key));
        }

        [Fact]
        public async Task GetGameByKey_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.GetByKeyAsync(key));
        }
    }
}
