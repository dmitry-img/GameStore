using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Enums;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Services;
using GameStore.BLL.Strategies.Sorting;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class GameServiceTests : BaseTest
    {
        private readonly GameService _gameService;

        public GameServiceTests()
        {
            _gameService = new GameService(
                MockUow.Object,
                Mapper,
                MockLogger.Object,
                GameFilterOperation,
                MockSortStrategyFactory.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfGetGameDTOs()
        {
            // Act
            var result = await _gameService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.IsAssignableFrom<IEnumerable<GetGameBriefDTO>>(result);
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

            MockUow.Setup(r => r.Games.Create(It.IsAny<Game>())).Callback((Game game) =>
            {
                game.Id = Games.Count + 1;
                Games.Add(game);
            });

            // Act
            await _gameService.CreateAsync(gameDTO);

            // Assert
            var allGames = await MockUow.Object.Games.GetAllAsync();
            Assert.Equal(5, allGames.Count());
            Assert.Equal(2, allGames.Last().Genres.Count);
            Assert.Equal(2, allGames.Last().PlatformTypes.Count);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var key = "test-key";
            var gameDTO = new UpdateGameDTO();

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
            var updatedGame = await MockUow.Object.Games.GetByKeyAsync(key);
            Assert.Equal("Updated Test Game", updatedGame.Name);
            Assert.Equal("Updated Test Description", updatedGame.Description);
            Assert.Single(updatedGame.Genres);
            Assert.Single(updatedGame.PlatformTypes);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
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
            MockUow.Verify(g => g.Games.Delete(It.IsAny<int>()), Times.Once);
            MockUow.Verify(g => g.SaveAsync(), Times.Once);
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
            MockUow.Setup(r => r.PlatformTypes.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => PlatformTypes.FirstOrDefault(g => g.Id == id));

            MockUow.Setup(g => g.Games.GetGamesByPlatformTypeAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => Games.Where(g =>
                    g.PlatformTypes.Select(pt => pt.Id).Contains(id)));

            var platformTypeId = 4;

            // Act
            var games = await _gameService.GetAllByPlatformTypeAsync(platformTypeId);

            // Assert
            Assert.Equal(3, games.Count());
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
            MockUow.Setup(r => r.Genres.GetAsync(It.IsAny<int>()))
               .ReturnsAsync((int id) => Genres.FirstOrDefault(g => g.Id == id));

            MockUow.Setup(g => g.Games.GetGamesByGenreAsync(It.IsAny<int>()))
               .ReturnsAsync((int id) => Games.Where(g =>
                   g.Genres.Select(genre => genre.Id).Contains(id)));

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

        [Fact]
        public void GetCount_ShouldReturnCount()
        {
            // Act
            var count = _gameService.GetCount();

            // Assert
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetFilteredAsync_ShouldReturnPaginationResult()
        {
            // Arrange
            var filter = new FilterGameDTO
            {
                NameFragment = "Warcraft",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 3 },
                PublisherIds = new List<int> { 1 },
                PriceFrom = 0,
                PriceTo = 100,
                DateFilterOption = DateFilterOption.None,
                SortOption = SortOption.New,
                PageSize = 1,
                PageNumber = 1
            };

            MockSortStrategyFactory.Setup(f => f.GetSortStrategy(It.IsAny<SortOption>()))
                .Returns(new NewSortStrategy());

            // Act
            var result = await _gameService.GetFilteredAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PaginationResult<GetGameBriefDTO>>(result);
            Assert.Single(result.Items);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(1, result.PageSize);
        }
    }
}
