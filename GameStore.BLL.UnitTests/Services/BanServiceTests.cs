using System.Threading.Tasks;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.Enums;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class BanServiceTests : BaseTest
    {
        private readonly BanService _banService;

        public BanServiceTests()
        {
            _banService = new BanService(MockLogger.Object);
        }

        [Fact]
        public async Task BanAsync_ShouldLogInfoCorrectly()
        {
            // Arrange
            var banDTO = new BanDTO
            {
                CommentId = 1,
                BanDuration = BanDuration.OneDay
            };

            // Act
            await _banService.BanAsync(banDTO);

            // Assert
            MockLogger.Verify(
                x => x.Info(It.Is<string>(s =>
                    s.Contains($"User who sent the comment({banDTO.CommentId}) has been banned! " +
                    $"Ban duration: {banDTO.BanDuration}"))), Times.Once);
        }
    }
}
