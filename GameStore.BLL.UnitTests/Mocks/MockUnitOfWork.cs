using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    internal class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> Get()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockGameRepositpry = MockGameRepository.GetRepository();
            var mockGenreRepository = MockGenreRepository.GetRepository();
            var mockPlatformTypeRepository = MockPlatformTypeRepository.GetRepository();
            var mockCommentRepository = MockCommentRepository.GetRepository();

            mockUow.Setup(r => r.Games).Returns(mockGameRepositpry.Object);
            mockUow.Setup(r => r.Genres).Returns(mockGenreRepository.Object);
            mockUow.Setup(r => r.PlatformTypes).Returns(mockPlatformTypeRepository.Object);
            mockUow.Setup(r => r.Comments).Returns(mockCommentRepository.Object);

            return mockUow;
        }
    }
}
