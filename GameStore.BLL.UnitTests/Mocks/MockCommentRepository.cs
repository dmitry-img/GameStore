using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.UnitTests.Mocks.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    internal class MockCommentRepository
    {
        public static Mock<IGenericRepository<Comment>> GetRepository()
        {
            var comments = new List<Comment>()
            {
                new Comment
                {
                    Id = 1,
                    Name = "Test Name",
                    Body = "Test Body",
                    GameId = 1,
                }
            };

            var mockRepo = new Mock<IGenericRepository<Comment>>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(comments);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
                comments.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    comment.Id = comments.Count + 1;
                    comments.Add(comment);
                });

            mockRepo.Setup(x => x.GetQuery()).Returns(new TestDbAsyncEnumerable<Comment>(comments));

            return mockRepo;
        }
    }
}
