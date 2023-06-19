using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.Api.Tests.Common;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class CommentsControllerTests : BaseTest
    {
        private const string TestKey = "test-key";
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<CreateCommentDTO>> _createCommentValidatorMock;
        private readonly CommentsController _commentsController;

        public CommentsControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _validationServiceMock = new Mock<IValidationService>();
            _createCommentValidatorMock = new Mock<IValidator<CreateCommentDTO>>();

            _commentsController = new CommentsController(
                _commentServiceMock.Object,
                _validationServiceMock.Object,
                _createCommentValidatorMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var commentDTO = new CreateCommentDTO
            {
                GameKey = TestKey,
                Body = "test-body"
            };

            // Act
            var result = await _commentsController.Create(commentDTO);

            // Assert
            _commentServiceMock.Verify(x => x.CreateAsync(It.IsAny<string>(), commentDTO), Times.Once);
        }

        [Fact]
        public async Task GetAllByGame_ShouldInvoke_GetAllByGameKeyAsync()
        {
            // Arrange
            var expectedGame = new List<GetCommentDTO> { new GetCommentDTO(), new GetCommentDTO() };
            var key = "test-key";
            _commentServiceMock.Setup(x => x.GetAllByGameKeyAsync(key)).ReturnsAsync(expectedGame);

            // Act
            var result = await _commentsController.GetAllByGame(TestKey);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult<IEnumerable<GetCommentDTO>>>(result);
            _commentServiceMock.Verify(x => x.GetAllByGameKeyAsync(TestKey), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldInvoke_CommentServiceWithCorrectId()
        {
            // Arrange
            var id = 1;

            _commentServiceMock
                .Setup(service => service.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _commentsController.Delete(id);

            // Assert
            _commentServiceMock.Verify(service => service.DeleteAsync(id), Times.Once);
        }
    }
}
