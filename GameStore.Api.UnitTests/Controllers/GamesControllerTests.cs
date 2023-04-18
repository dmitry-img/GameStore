﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
            // Act
            var result = await _gamesController.GetAll();

            // Assert
            _gameServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByKey_ShouldInvoke_GetByKeyAsync()
        {
            // Act
            var result = await _gamesController.GetByKey(TestKey);

            // Assert
            _gameServiceMock.Verify(x => x.GetByKeyAsync(TestKey), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var gameDTO = new CreateGameDTO();

            // Act
            var result = await _gamesController.Create(gameDTO);

            // Assert
            _gameServiceMock.Verify(x => x.CreateAsync(gameDTO), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldInvoke_UpdateAsync()
        {
            // Arrange
            var gameDTO = new UpdateGameDTO();

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
    }
}