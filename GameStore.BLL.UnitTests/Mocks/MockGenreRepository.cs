using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    class MockGenreRepository
    {
        public static Mock<IGenericRepository<Genre>> GetRepository()
        {
            var genres = new List<Genre>()
            {
                new Genre() { Id = 1, Name = "Strategy" },
                new Genre() { Id = 2, Name = "RTS", ParentGenreId = 1 },
                new Genre() { Id = 3, Name = "TBS", ParentGenreId = 1  },
                new Genre() { Id = 4, Name = "RPG" },
                new Genre() { Id = 5, Name = "Sports" },
                new Genre() { Id = 6, Name = "Races"},
                new Genre() { Id = 7, Name = "Rally", ParentGenreId = 6 },
                new Genre() { Id = 8, Name = "Arcade", ParentGenreId = 6 },
                new Genre() { Id = 9, Name = "Formula", ParentGenreId = 6 },
                new Genre() { Id = 10, Name = "Off-road", ParentGenreId = 6 },
                new Genre() { Id = 11, Name = "Action" },
                new Genre() { Id = 12, Name = "FPS", ParentGenreId = 11 },
                new Genre() { Id = 13, Name = "TPS", ParentGenreId = 11 },
                new Genre() { Id = 14, Name = "Adventure" },
                new Genre() { Id = 15, Name = "Puzzle&Skill" },
                new Genre() { Id = 16, Name = "Misc" },
            };

            var mockRepo = new Mock<IGenericRepository<Genre>>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(genres);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => genres.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<Genre>())).Callback((Genre genre) =>
            {
                genres.Add(genre);
            });

            mockRepo.Setup(r => r.FilterAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
                .ReturnsAsync((Expression<Func<Genre, bool>> expression) =>
                {
                    return genres.Where(expression.Compile()).ToList();
                });

            return mockRepo;

        }
    }
}
