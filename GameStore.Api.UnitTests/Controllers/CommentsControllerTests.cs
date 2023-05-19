using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Validators;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class CommentsControllerTests
    {
        private const string TestKey = "test-key";
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly Mock<IBanService> _banServiceMock;
        private readonly IValidationService _validationService;
        private readonly CommentsController _commentsController;
        private readonly IValidator<CreateCommentDTO> _createCommentValidator;

        public CommentsControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _banServiceMock = new Mock<IBanService>();
            _createCommentValidator = new CreateCommentDTOValidator();
            _validationService = new ValidationService();
            _commentsController = new CommentsController(
                _commentServiceMock.Object,
                _validationService,
                _banServiceMock.Object,
                _createCommentValidator);
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
        public async Task Ban_ShouldInvoke_BanServiceWithCorrectDto()
        {
            // Arrange
            var banDto = new BanDTO();

            _banServiceMock
                .Setup(service => service.BanAsync(It.IsAny<BanDTO>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _commentsController.Ban(banDto);

            // Assert
            _banServiceMock.Verify(service => service.BanAsync(banDto), Times.Once);
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
