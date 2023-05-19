using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Pipelines;
using GameStore.BLL.Profiles;
using GameStore.BLL.UnitTests.Mocks.Common;
using GameStore.DAL.CacheEntities;
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

            MockSortStrategyFactory = new Mock<ISortStrategyFactory>();

            MockPaymentStrategyFactory = new Mock<IPaymentStrategyFactory>();

            MockLogger = new Mock<ILog>();

            GameFilterOperation = new GameFilterOperations();

            MockShoppingCartCash = new Mock<IDistributedCache<ShoppingCart>>();
            MockShoppingCartCash.Setup(sc => sc.GetAsync(It.IsAny<string>())).ReturnsAsync(ShoppingCart);
            MockShoppingCartCash.Setup(r => r.SetAsync(It.IsAny<string>(), It.IsAny<ShoppingCart>()))
                .Callback((string key, ShoppingCart modifiedShoppingCart) =>
                {
                    ShoppingCart = modifiedShoppingCart;
                });

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

            MockUow.Setup(r => r.Publishers.GetAllAsync()).ReturnsAsync(Publishers);
            MockUow.Setup(u => u.Publishers.GetQuery()).Returns(new TestDbAsyncEnumerable<Publisher>(Publishers));

            MockUow.Setup(u => u.Comments.GetQuery()).Returns(new TestDbAsyncEnumerable<Comment>(Comments));

            MockUow.Setup(u => u.Orders.GetQuery()).Returns(new TestDbAsyncEnumerable<Order>(Orders));
        }

        protected IMapper Mapper { get; }

        protected Mock<ILog> MockLogger { get; }

        protected Mock<IUnitOfWork> MockUow { get; set; }

        protected Mock<IDistributedCache<ShoppingCart>> MockShoppingCartCash { get; set; }

        protected IGameFilterOperations GameFilterOperation { get; set; }

        protected Mock<ISortStrategyFactory> MockSortStrategyFactory { get; set; }

        protected Mock<IPaymentStrategyFactory> MockPaymentStrategyFactory { get; set; }

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
                },
                Price = 50,
                Discontinued = false,
                PublisherId = 1,
                UnitsInStock = 50
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
                IsDeleted = true,
                Price = 10,
                PublisherId = 2,
                UnitsInStock = 50
            },
            new Game()
            {
                Id = 3,
                Name = "GTA V",
                Key = "55aa36g4-16b0-4eec-8c27-39b54e67664d",
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
                IsDeleted = false,
                Price = 100,
                PublisherId = 2,
                UnitsInStock = 50
            },
            new Game()
            {
                Id = 4,
                Name = "Age of Empires II",
                Key = "33bb26e3-16b0-4eec-8c27-39b54e67664d",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
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
                    new PlatformType() { Id = 3, Type = "Desktop" },
                },
                IsDeleted = false,
                Price = 30,
                PublisherId = 2,
                UnitsInStock = 50
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
                ChildComments = new List<Comment>()
                {
                    new Comment
                    {
                        Id = 2,
                        Name = "Test Name Child1",
                        Body = "Test Body  Child1",
                        GameId = 1,
                        ParentCommentId = 1
                    },
                    new Comment
                    {
                        Id = 3,
                        Name = "Test Name Child2",
                        Body = "Test Body Child2",
                        GameId = 1,
                        ParentCommentId = 1
                    }
                }
            },
        };

        protected ShoppingCart ShoppingCart { get; set; } = new ShoppingCart()
        {
            Items = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem
                {
                    GameKey = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                    GameName = "Warcraft III",
                    GamePrice = 1,
                    Quantity = 1
                },
                new ShoppingCartItem
                {
                    GameKey = "55aa36g4-16b0-4eec-8c27-39b54e67664d",
                    GameName = "GTA V",
                    GamePrice = 100,
                    Quantity = 2
                }
            }
        };

        protected List<Publisher> Publishers { get; set; } = new List<Publisher>
        {
            new Publisher()
            {
                CompanyName = "Blizzard Entertainment",
                Description = "Blizzard description...",
                HomePage = "https://www.blizzard.com/"
            },
            new Publisher()
            {
                CompanyName = "Rockstar",
                Description = "Rockstar description...",
                HomePage = "https://www.rockstargames.com/"
            },
            new Publisher()
            {
                CompanyName = "Electronic Arts",
                Description = "Electronic Arts description...",
                HomePage = "https://www.ea.com/"
            }
        };

        protected List<Order> Orders { get; set; } = new List<Order>()
        {
            new Order()
            {
                Id = 1,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        Price = 500,
                        Quantity = 2,
                        Game = new Game { UnitsInStock = 5 }
                    },
                    new OrderDetail()
                    {
                        Price = 100,
                        Quantity = 5,
                        Game = new Game { UnitsInStock = 50 }
                    }
                }
            },
            new Order()
            {
                Id = 2,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        Price = 50,
                        Quantity = 1,
                        Game = new Game { UnitsInStock = 100 }
                    },
                    new OrderDetail()
                    {
                        Price = 100,
                        Quantity = 1,
                        Game = new Game { UnitsInStock = 100 }
                    }
                }
            }
        };
    }
}
