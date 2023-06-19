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
using GameStore.DAL.Enums;
using GameStore.DAL.Interfaces;
using GameStore.Shared.Infrastructure;
using log4net;
using Moq;
using static GameStore.Shared.Infrastructure.Constants;

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

            MockUow.Setup(r => r.Users.GetQuery()).Returns(new TestDbAsyncEnumerable<User>(Users));

            MockUow.Setup(r => r.Roles.GetQuery()).Returns(new TestDbAsyncEnumerable<Role>(Roles));
            MockUow.Setup(r => r.Roles.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => Roles.FirstOrDefault(r => r.Id == id));

            MockUow.Setup(r => r.PlatformTypes.GetAllAsync()).ReturnsAsync(PlatformTypes);
            MockUow.Setup(r => r.PlatformTypes.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => PlatformTypes.FirstOrDefault(g => g.Id == id));

            MockUow.Setup(r => r.Genres.GetQuery()).Returns(new TestDbAsyncEnumerable<Genre>(Genres));
            MockUow.Setup(r => r.Genres.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) => Genres.FirstOrDefault(g => g.Id == id));

            MockConfigurationWrapper = new Mock<IConfigurationWrapper>();
            MockConfigurationWrapper.Setup((x) => x.HasKey(It.IsAny<string>())).Returns(true);
            MockConfigurationWrapper.Setup((x) => x.GetValue(It.Is<string>(s => s == JwtSecret))).Returns("8455c5aa-5f60-44de-b2de-a2c8cb9270e6");
            MockConfigurationWrapper.Setup((x) => x.GetValue(It.Is<string>(s => s == AccessTokenExpirationDateInMinutesName))).Returns("10");
            MockConfigurationWrapper.Setup((x) => x.GetValue(It.Is<string>(s => s == RefreshTokenExpirationDateInDaysName))).Returns("10");
        }

        protected IMapper Mapper { get; }

        protected Mock<ILog> MockLogger { get; }

        protected Mock<IUnitOfWork> MockUow { get; set; }

        protected Mock<IDistributedCache<ShoppingCart>> MockShoppingCartCash { get; set; }

        protected Mock<ISortStrategyFactory> MockSortStrategyFactory { get; set; }

        protected Mock<IPaymentStrategyFactory> MockPaymentStrategyFactory { get; set; }

        protected Mock<IConfigurationWrapper> MockConfigurationWrapper { get; set; }

        protected string RegularUserObjectId { get; set; } = "afdeb0ed-bd13-48b3-8075-4898893c565d";

        protected string AdministratorObjectId { get; set; } = "f8b86ce6-bbd2-440b-8dee-3acaa2057512";

        protected string ModeratorObjectId { get; set; } = "5bfa5c2b-8588-45c0-9efa-f2f1a928e76b";

        protected string ManagerObjectId { get; set; } = "6804d719-eaa5-4113-81de-91d75ff0b5e7";

        protected string PublisherObjectId { get; set; } = "b53ee89f-23c3-4913-9cd5-a76dabea98b4";

        protected string BannedRegularUserObjectId { get; set; } = "afdeb0ed-1111-48b3-8075-4898893c565d";

        protected List<Role> Roles { get; set; } = new List<Role>
        {
            new Role { Id = 1, Name = "Administrator" },
            new Role { Id = 2, Name = "Manager" },
            new Role { Id = 3, Name = "Moderator" },
            new Role { Id = 4, Name = "User" },
            new Role { Id = 5, Name = "Publisher" }
        };

        protected List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                ObjectId = "f8b86ce6-bbd2-440b-8dee-3acaa2057512",
                Username = "administrator",
                Email = "admin@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 1,
                Role = new Role
                {
                    Name = "Administrator"
                },
                RefreshToken = "2c32555a-e8ba-41ed-a95e-1075238cd476"
            },
            new User
            {
                Id = 2,
                ObjectId = "6804d719-eaa5-4113-81de-91d75ff0b5e7",
                Username = "manager",
                Email = "manager@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 2,
                Role = new Role
                {
                    Name = "Manager"
                },
                RefreshToken = "28c92555-ca26-4ab9-8f73-f0537803198f"
            },
            new User
            {
                Id = 3,
                ObjectId = "5bfa5c2b-8588-45c0-9efa-f2f1a928e76b",
                Username = "moderator",
                Email = "moderator@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 3,
                Role = new Role
                {
                    Name = "Moderator"
                },
                RefreshToken = "64a23142-ea8a-47fc-a786-ad368acbd0de"
            },
            new User
            {
                Id = 4,
                ObjectId = "afdeb0ed-bd13-48b3-8075-4898893c565d",
                Username = "regularUser",
                Email = "user@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 4,
                Role = new Role
                {
                    Name = "User"
                },
                RefreshToken = "9027deea-13f6-4d04-9275-b2af7420bdbd"
            },
            new User
            {
                Id = 4,
                ObjectId = "afdeb0ed-1111-48b3-8075-4898893c565d",
                Username = "bannedregularUser",
                Email = "banneduser@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 4,
                Role = new Role
                {
                    Name = "User"
                },
                RefreshToken = "9027deea-1111-4d04-9275-b2af7420bdbd",
                BanEndDate = DateTime.UtcNow.AddHours(1)
            },
            new User
            {
                Id = 5,
                ObjectId = "b53ee89f-23c3-4913-9cd5-a76dabea98b4",
                Username = "blizzard",
                Email = "blizzard@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 5,
                Role = new Role
                {
                    Name = "Publisher"
                },
                RefreshToken = "cac4c90a-d470-4c94-bcc7-358929984683"
            },
            new User
            {
                Id = 6,
                ObjectId = "989795bd-032e-4cf6-89bb-dba58322839d",
                Username = "rockstar",
                Email = "rockstar@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 5,
                Role = new Role
                {
                    Name = "Publisher"
                },
                RefreshToken = "d20dfc10-3aa2-4681-b18f-2d2f2ee3afb5"
            },
            new User
            {
                Id = 7,
                ObjectId = "c9a9c312-0dd8-4a69-8e76-cc48631479d9",
                Username = "electronic-arts",
                Email = "electronic-arts@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 5,
                Role = new Role
                {
                    Name = "Publisher"
                },
                RefreshToken = "3b5e76e7-aa5f-4764-ad52-1365e13d33b1"
            },
            new User
            {
                Id = 8,
                ObjectId = "afa9c312-0dd8-4a69-8e76-ff588631479d9",
                Username = "free",
                Email = "free@example.com",
                Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                RoleId = 5,
                Role = new Role
                {
                    Name = "Publisher"
                },
                RefreshToken = "2cd3c699-7b92-4b87-a5af-b5451eb9e099"
            }
        };

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
                PublisherId = 1,
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
                Body = "Test Body",
                GameId = 1,
                User = new User
                {
                    Id = 4
                },
                ChildComments = new List<Comment>()
                {
                    new Comment
                    {
                        Id = 2,
                        Body = "Test Body  Child1",
                        GameId = 1,
                        ParentCommentId = 1
                    },
                    new Comment
                    {
                        Id = 3,
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
                Id = 1,
                UserId = 5,
                CompanyName = "Blizzard Entertainment",
                Description = "Blizzard description...",
                HomePage = "https://www.blizzard.com/",
                Games = new List<Game>
                {
                    new Game
                    {
                         Key = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                    },
                    new Game()
                    {
                         Key = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
                    }
                },
                User = new User
                {
                    ObjectId = "b53ee89f-23c3-4913-9cd5-a76dabea98b4"
                }
            },
            new Publisher()
            {
                Id = 2,
                UserId = 6,
                CompanyName = "Rockstar",
                Description = "Rockstar description...",
                HomePage = "https://www.rockstargames.com/"
            },
            new Publisher()
            {
                Id = 3,
                UserId = 7,
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
                        Game = new Game
                        {
                            Key = "55aa36g4-16b0-4eec-8c27-39b54e67664d",
                            UnitsInStock = 5
                        }
                    },
                    new OrderDetail()
                    {
                        Price = 100,
                        Quantity = 5,
                        Game = new Game
                        {
                            Key = "78cc36g4-16b0-4eec-8c27-39b54e67664d",
                            UnitsInStock = 50
                        }
                    }
                },
                OrderDate = DateTime.UtcNow,
                OrderState = OrderState.Paid
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
                },
                OrderDate = DateTime.UtcNow,
                OrderState = OrderState.Paid
            }
        };
    }
}
