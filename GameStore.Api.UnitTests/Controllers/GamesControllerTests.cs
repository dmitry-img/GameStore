using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class GamesControllerTests
    {
        private const string TestKey = "test-key";
        private readonly Mock<IGameService> _gameServiceMock;
        private readonly GamesController _gamesController;

        public GamesControllerTests()
        {
            _gameServiceMock = new Mock<IGameService>();
            _gamesController = new GamesController(_gameServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldInvoke_GetAllAsync()
        {
            // Arrange
            var expectedGames = new List<GetGameBriefDTO> { new GetGameBriefDTO(), new GetGameBriefDTO() };
            _gameServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedGames);

            // Act
            var result = await _gamesController.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<GetGameBriefDTO>>>(result);
            _gameServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByKey_ShouldInvoke_GetByKeyAsync()
        {
            // Arrange
            var expectedGame = new GetGameDTO();
            var key = "test-key";
            _gameServiceMock.Setup(x => x.GetByKeyAsync(key)).ReturnsAsync(expectedGame);

            // Act
            var result = await _gamesController.GetByKey(TestKey);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<GetGameDTO>>(result);
            _gameServiceMock.Verify(x => x.GetByKeyAsync(TestKey), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var gameDTO = new CreateGameDTO()
            {
                Name = "Test",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing " +
                "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 },
            };

            // Act
            var result = await _gamesController.Create(gameDTO);

            // Assert
            _gameServiceMock.Verify(x => x.CreateAsync(gameDTO), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldInvoke_UpdateAsync()
        {
            // Arrange
            var gameDTO = new UpdateGameDTO()
            {
                Name = "Test",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing " +
                "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                GenreIds = new List<int> { 1 },
                PlatformTypeIds = new List<int> { 1 },
            };

            // Act
            var result = await _gamesController.Update(TestKey, gameDTO);

            // Assert
            _gameServiceMock.Verify(x => x.UpdateAsync(TestKey, gameDTO), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldInvoke_DeleteAsync()
        {
            // Act
            var result = await _gamesController.Delete(TestKey);

            // Assert
            _gameServiceMock.Verify(x => x.DeleteAsync(TestKey), Times.Once);
        }

        [Fact]
        public async Task Download_ShouldReturn_File()
        {
            // Arrange
            var game = new GetGameDTO
            {
                Key = TestKey,
                Name = "Test Game",
                Description = "This is a test game",
            };

            _gameServiceMock.Setup(x => x.GetByKeyAsync(TestKey)).ReturnsAsync(game);
            _gameServiceMock.Setup(x => x.GetGameFileAsync(TestKey))
                .ReturnsAsync(new MemoryStream(Encoding.ASCII.GetBytes(game.Name)));

            // Act
            var result = await _gamesController.Download(TestKey);

            // Assert
            _gameServiceMock.Verify(x => x.GetGameFileAsync(TestKey), Times.Once);
            _gameServiceMock.Verify(x => x.GetByKeyAsync(TestKey), Times.Once);

            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(
                new MediaTypeHeaderValue("application/octet-stream"),
                result.Content.Headers.ContentType);
            Assert.Equal(
                new ContentDispositionHeaderValue("attachment") { FileName = $"{game.Name}.bin" },
                result.Content.Headers.ContentDisposition);
        }

        [Fact]
        public async Task Create_WithInvalidDTO_ShouldReturn_BadRequest()
        {
            // Arrange
            var game = new CreateGameDTO();

            // Act
            var result = await _gamesController.Create(game);

            // Assert
            Assert.IsType<BadRequestErrorMessageResult>(result);
        }

        [Fact]
        public async Task Update_WithInvalidDTO_ShouldReturn_BadRequest()
        {
            // Arrange
            var game = new UpdateGameDTO();
            var gameKey = "test-key";

            // Act
            var result = await _gamesController.Update(gameKey, game);

            // Assert
            Assert.IsType<BadRequestErrorMessageResult>(result);
        }
    }
}
