using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.UnitTests.Mocks.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;

namespace GameStore.BLL.UnitTests.Mocks
{
    internal class MockGameRepository
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
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Name = "Test Name",
                            Body = "Test Body",
                        }
                    }
                },
                new Game()
                {
                    Id = 2,
                    Name = "Warcraft IV",
                    Key = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Genres = new List<Genre>
                    {
                        new Genre()
                        {
                            Id = 1,
                            Name = "Strategy",
                        },
                    },
                    PlatformTypes = new List<PlatformType>()
                    {
                        new PlatformType() { Id = 4, Type = "Console" },
                    },
                    IsDeleted = true
                }
            };

            var mockRepo = new Mock<IGameRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(games);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
                games.FirstOrDefault(g => g.Id == id));

            mockRepo.Setup(r => r.Create(It.IsAny<Game>())).Callback((Game game) =>
            {
                game.Id = games.Count + 1;
                games.Add(game);
            });

            mockRepo.Setup(r => r.GetByKeyAsync(It.IsAny<string>()))
                .ReturnsAsync((string key) => games.FirstOrDefault(g => g.Key == key));

            mockRepo.Setup(g => g.GetQuery()).Returns(new TestDbAsyncEnumerable<Game>(games));

            mockRepo.Setup(g => g.GetGamesByGenreAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => games.Where(g =>
                    g.Genres.Select(genre => genre.Id).Contains(id)));

            mockRepo.Setup(g => g.GetGamesByPlatformTypeAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => games.Where(g =>
                    g.PlatformTypes.Select(pt => pt.Id).Contains(id)));

            return mockRepo;
        }
    }
}
