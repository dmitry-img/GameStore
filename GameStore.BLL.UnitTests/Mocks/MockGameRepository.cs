using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    class MockGameRepository
    {
        public static Mock<IGameRepository> GetRepository()
        {
            var games = new List<Game>()
            {
                new Game()
                {
                    Id = 1,
                    Name = "Warcraft III",
                    Key = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Genres = new List<Genre>
                    {
                        new Genre()
                        {
                            Id = 1,
                            Name = "Strategy",
                        },
                        new Genre() 
                        { 
                            Id = 2, 
                            Name = "RTS",
                            ParentGenreId = 1
                        },
                    },
                    PlatformTypes = new List<PlatformType>()
                    {
                        new PlatformType() { Id = 3, Type = "Desktop" },
                        new PlatformType() { Id = 4, Type = "Console" },
                    }
                },
            };

            var mockRepo = new Mock<IGameRepository>();

            mockRepo.Setup(r => r.GetAll()).Returns(games);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).Returns((int id) => 
                games.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<Game>())).Callback((Game game) =>
            {
                game.Id = games.Count + 1;
                games.Add(game);
            });

            mockRepo.Setup(r => r.GetByKeyAsync(It.IsAny<string>()))
                .ReturnsAsync((string key) =>
                {
                    return games.FirstOrDefault(g => g.Key == key);
                });

            return mockRepo;
        }
    }
}
