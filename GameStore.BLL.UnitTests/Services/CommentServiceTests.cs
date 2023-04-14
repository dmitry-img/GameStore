using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Comment;
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
    public class CommentServiceTests 
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly IMapper _mapper;
        private readonly Mock<ILog> _loggerMock;
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _mockUow = MockUnitOfWork.Get();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _loggerMock = new Mock<ILog>();

            _commentService = new CommentService(_mockUow.Object, _mapper, _loggerMock.Object);
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

            // Act
            await _commentService.CreateAsync(commentDTO);

            // Assert
            Assert.Equal(2, (await _mockUow.Object.Games.GetByKeyAsync(gameKey)).Comments.Count);
            _mockUow.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_GameNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var gameKey = "test-key";

            var commentDTO = new CreateCommentDTO
            {
                Name = "My Test Comment",
                Body = "My Test Body",
                GameKey = gameKey,
                ParentCommentId = 1
            };

            // Act& Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _commentService.CreateAsync(commentDTO));
        }

        [Fact]
        public async Task GetAllByGameKeyAsync_ShouldReturnListOfGetGameDTOs()
        {
            //Arrange
            var gameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d";

            //Act
            var comments = await _commentService.GetAllByGameKeyAsync(gameKey);

            //Assert
            Assert.NotEmpty(comments);
        }

        [Fact]
        public async Task GetAllByGameKeyAsync_GameNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var gameKey = "test-key";

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _commentService.GetAllByGameKeyAsync(gameKey));
        }
    }
}
