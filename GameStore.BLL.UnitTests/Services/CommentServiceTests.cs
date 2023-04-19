using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using log4net;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class CommentServiceTests : BaseTest
    {
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _commentService = new CommentService(MockUow.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidCommenteDTO_CreatesComment()
        {
            // Arrange
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            var commentDTO = new CreateCommentDTO
            {
                 Name = "My Test Comment",
                 Body = "My Test Body",
                 GameKey = gameKey,
            };

            MockUow.Setup(r => r.Comments.Create(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    comment.Id = Comments.Count + 1;
                    Comments.Add(comment);
                });

            // Act
            await _commentService.CreateAsync(commentDTO);

            // Assert
            Assert.Equal(2, (await MockUow.Object.Games.GetByKeyAsync(gameKey)).Comments.Count);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var commentDTO = new CreateCommentDTO();

            // Act& Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _commentService.CreateAsync(commentDTO));
        }

        [Fact]
        public async Task GetAllByGameKeyAsync_ShouldReturnListOfGetGameDTOs()
        {
            // Arrange
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            // Act
            var comments = await _commentService.GetAllByGameKeyAsync(gameKey);

            // Assert
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task GetAllByGameKeyAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var gameKey = "test-key";

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _commentService.GetAllByGameKeyAsync(gameKey));
        }
    }
}
