using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Mocks;
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
        public async Task GetAllAsync_ShouldReturnListOfGetGameDTOs()
        {
            // Act
            var result = await _gameService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<GetGameDTO>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateAsync_WithValidGameDTO_CreatesGame()
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
            Assert.Equal(3, (await _mockUow.Object.Games.GetAllAsync()).Count());
            Assert.Equal(2, (await _mockUow.Object.Games.GetAllAsync()).Last().Genres.Count);
            Assert.Equal(2, (await _mockUow.Object.Games.GetAllAsync()).Last().PlatformTypes.Count);
            _mockUow.Verify(uow => uow.SaveAsync(), Times.Once);
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
        public async Task UpdateAsync_WithValidUpdateGameDTO_UpdatesGame()
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

            // Assert
            Assert.Equal(
                "Updated Test Game",
                (await _mockUow.Object.Games.GetByKeyAsync(key)).Name);
            Assert.Equal(
                "Updated Test Description",
                (await _mockUow.Object.Games.GetByKeyAsync(key)).Description);
            Assert.Single((await _mockUow.Object.Games.GetByKeyAsync(key)).Genres);
            Assert.Single((await _mockUow.Object.Games.GetByKeyAsync(key)).PlatformTypes);
            _mockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.DeleteAsync(key));
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteGame()
        {
            // Arrange
            var key = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            // Act
            await _gameService.DeleteAsync(key);

            // Assert
            _mockUow.Verify(g => g.Games.Delete(It.IsAny<int>()), Times.Once);
            _mockUow.Verify(g => g.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetGameByKey_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.GetByKeyAsync(key));
        }

        [Fact]
        public async Task GetGameByKey_ShouldReturnGetGameDTO()
        {
            // Arrange
            var key = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            // Act
            var game = await _gameService.GetByKeyAsync(key);

            // Assert
            Assert.NotNull(game);
            Assert.IsAssignableFrom<GetGameDTO>(game);
            Assert.Equal(key, game.Key);
        }

        [Fact]
        public async Task GetAllByPlatformTypeAsync_WithValidPlatformTypeId_ReturnsListOfGetGameDTOs()
        {
            // Arrange
            var platformTypeId = 4;

            // Act
            var games = await _gameService.GetAllByPlatformTypeAsync(platformTypeId);

            // Assert
            Assert.Equal(2, games.Count());
        }

        [Fact]
        public async Task GetAllByPlatformTypeAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var platformTypeId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.GetAllByPlatformTypeAsync(platformTypeId));
        }

        [Fact]
        public async Task GetAllByGenreAsync_WithValidGenreId_ReturnsListOfGetGameDTOs()
        {
            // Arrange
            var genreId = 2;

            // Act
            var games = await _gameService.GetAllByGenreAsync(genreId);

            // Assert
            Assert.Single(games);
        }

        [Fact]
        public async Task GetAllByGenreAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var platformTypeId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.GetAllByGenreAsync(platformTypeId));
        }

        [Fact]
        public async Task GetGameFileAsync_WithValidGameKey_ReturnsMemoryStreamWithGameName()
        {
            // Arrange
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";
            var expectedBytes = Encoding.ASCII.GetBytes("Warcraft III");

            // Act
            var gottenBytes = await _gameService.GetGameFileAsync(gameKey);

            // Assert
            Assert.Equal(expectedBytes, gottenBytes.ToArray());
        }

        [Fact]
        public async Task GetGameFileAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var gameKey = "test-key";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _gameService.GetGameFileAsync(gameKey));
        }
    }
}
