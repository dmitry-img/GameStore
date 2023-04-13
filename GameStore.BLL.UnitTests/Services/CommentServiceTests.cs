using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Comment;
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
        public async Task CreateAsync_ValidGameDTO_CreatesGame()
        {
            // Arrange
            var commentDTO = new CreateCommentDTO
            {
                 Name = "My Test Comment",
                 Body = "My Test Body",
                 GameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                 ParentCommentId = 1
            };

            var expectedCommentId = 2;
            var expectedGameId = 1;

            // Act
            await _commentService.CreateAsync(commentDTO);

            // Assert
            Assert.Equal(2, _mockUow.Object.Comments.GetAll().Count());
            _loggerMock.Verify(l => l.Info($"Comment({expectedCommentId}) " +
                $"was created for game({expectedGameId})"), Times.Once);
        }
    }
}
