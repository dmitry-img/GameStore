﻿using System.Threading.Tasks;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.Tests.Controllers
{
    public class CommentsControllerTests
    {
        private const string TestKey = "test-key";
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly CommentsController _commentsController;

        public CommentsControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _commentsController = new CommentsController(_commentServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var commentDTO = new CreateCommentDTO
            {
                GameKey = TestKey,
                Name = "test-name",
                Body = "test-body"
            };

            // Act
            var result = await _commentsController.Create(commentDTO);

            // Assert
            _commentServiceMock.Verify(x => x.CreateAsync(commentDTO), Times.Once);
        }

        [Fact]
        public async Task GetAllByGame_ShouldInvoke_GetAllByGameKeyAsync()
        {
            // Act
            var result = await _commentsController.GetAllByGame(TestKey);

            // Assert
            _commentServiceMock.Verify(x => x.GetAllByGameKeyAsync(TestKey), Times.Once);
        }
    }
}
