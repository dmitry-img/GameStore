using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    class MockCommentRepository
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

            mockRepo.Setup(r => r.GetAll()).Returns(comments);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).Returns((int id) =>
                comments.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    comment.Id = comments.Count + 1;
                    comments.Add(comment);
                });

            return mockRepo;
        }
    }
}
