using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Profiles;
using GameStore.BLL.UnitTests.Mocks.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
using Moq;

namespace GameStore.BLL.UnitTests.Common
{
    public class BaseTest
    {
        public BaseTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            Mapper = mapperConfig.CreateMapper();

            MockLogger = new Mock<ILog>();

            MockUow = new Mock<IUnitOfWork>();
            MockUow.Setup(u => u.Games.GetAllAsync()).ReturnsAsync(Games);
            MockUow.Setup(u => u.Games.GetQuery()).Returns(new TestDbAsyncEnumerable<Game>(Games));
            MockUow.Setup(r => r.Games.GetByKeyAsync(It.IsAny<string>()))
                .ReturnsAsync((string key) => Games.FirstOrDefault(g => g.Key == key));

            MockUow.Setup(r => r.PlatformTypes.FilterAsync(It.IsAny<Expression<Func<PlatformType, bool>>>()))
               .ReturnsAsync((Expression<Func<PlatformType, bool>> expression) =>
               {
                   return PlatformTypes.Where(expression.Compile()).ToList();
               });

            MockUow.Setup(r => r.Genres.FilterAsync(It.IsAny<Expression<Func<Genre, bool>>>()))
                .ReturnsAsync((Expression<Func<Genre, bool>> expression) =>
                {
                    return Genres.Where(expression.Compile()).ToList();
                });
        }

        protected IMapper Mapper { get; }

        protected Mock<ILog> MockLogger { get; }

        protected Mock<IUnitOfWork> MockUow { get; set; }

        protected List<Game> Games { get; set; } = new List<Game>()
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

        protected List<Genre> Genres { get; set; } = new List<Genre>()
        {
            new Genre() { Id = 1, Name = "Strategy" },
            new Genre() { Id = 2, Name = "RTS", ParentGenreId = 1 },
            new Genre() { Id = 3, Name = "TBS", ParentGenreId = 1 },
            new Genre() { Id = 4, Name = "RPG" },
            new Genre() { Id = 5, Name = "Sports" },
            new Genre() { Id = 6, Name = "Races" },
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

        protected List<PlatformType> PlatformTypes { get; set; } = new List<PlatformType>()
        {
            new PlatformType() { Id = 1, Type = "Mobile" },
            new PlatformType() { Id = 2, Type = "Browser" },
            new PlatformType() { Id = 3, Type = "Desktop" },
            new PlatformType() { Id = 4, Type = "Console" },
        };

        protected List<Comment> Comments { get; set; } = new List<Comment>()
        {
            new Comment
            {
                Id = 1,
                Name = "Test Name",
                Body = "Test Body",
                GameId = 1,
            }
        };
    }
}
