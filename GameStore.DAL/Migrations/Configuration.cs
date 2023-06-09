namespace GameStore.DAL.Migrations
{
    using GameStore.DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStore.DAL.Data.GameStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStore.DAL.Data.GameStoreDbContext context)
        {
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(new List<Genre>()
                {
                    new Genre()
                    {
                        Name = "Strategy",
                        ChildGenres = new List<Genre>()
                        {
                            new Genre() { Name = "RTS" },
                            new Genre() { Name = "TBS" },
                        }
                    },
                    new Genre() { Name = "RPG" },
                    new Genre() { Name = "Sports" },
                    new Genre()
                    {
                        Name = "Races",
                        ChildGenres = new List<Genre>()
                        {
                            new Genre() { Name = "Rally" },
                            new Genre() { Name = "Arcade" },
                            new Genre() { Name = "Formula" },
                            new Genre() { Name = "Off-road" },
                        }
                    },
                    new Genre()
                    {
                        Name = "Action",
                        ChildGenres = new List<Genre>()
                        {
                            new Genre() { Name = "FPS" },
                            new Genre() { Name = "TPS" }
                        }
                    },
                    new Genre() { Name = "Adventure" },
                    new Genre() { Name = "Puzzle&Skill" },
                    new Genre() { Name = "Misc" },
                    new Genre() { Name = "Other" }
                });
            }

            if (!context.PlatformTypes.Any())
            {
                context.PlatformTypes.AddRange(new List<PlatformType>()
                {
                    new PlatformType() { Type = "Mobile" },
                    new PlatformType() { Type = "Browser" },
                    new PlatformType() { Type = "Desktop" },
                    new PlatformType() { Type = "Console" },
                });
            }

            if (!context.Role.Any())
            {
                context.Role.AddRange(new List<Role>
                    {
                        new Role { Id = 1, Name = "Administrator" },
                        new Role { Id = 2, Name = "Manager" },
                        new Role { Id = 3, Name = "Moderator" },
                        new Role { Id = 4, Name = "User" },
                        new Role { Id = 5, Name = "Publisher" }
                    });
            }

            if (!context.Users.Any())
            {
                // The password "q1w2e3r4" is set for all the users
                context.Users.AddRange(new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Username = "administrator",
                        Email = "admin@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 1
                    },
                    new User
                    {
                        Id = 2,
                        Username = "manager",
                        Email = "manager@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 2
                    },
                    new User
                    {
                        Id = 3,
                        Username = "moderator",
                        Email = "moderator@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 3
                    },
                    new User
                    {
                        Id = 4,
                        Username = "regularUser",
                        Email = "user@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 4
                    },
                    new User {
                        Id = 5,
                        Username = "blizzard",
                        Email = "blizzard@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 5
                    },
                    new User {
                        Id = 6,
                        Username = "rockstar",
                        Email = "rockstar@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 5
                    },
                    new User {
                        Id = 7,
                        Username = "electronic-arts",
                        Email = "electronic-arts@example.com",
                        Password = "$2a$11$ZIUkolq17gMt8KlJYOauU.PT9uRrkiLNDz6oR1lepFqqg12IU4jIW",
                        RoleId = 5
                    }
                });
            }

            if (!context.Publishers.Any())
            {
                context.Publishers.AddRange(new List<Publisher>
                {
                    new Publisher()
                    {
                        UserId = 5,
                        CompanyName = "Blizzard Entertainment",
                        Description = "Blizzard description...",
                        HomePage = "https://www.blizzard.com/"
                    },
                    new Publisher()
                    {
                        UserId = 6,
                        CompanyName = "Rockstar",
                        Description = "Rockstar description...",
                        HomePage = "https://www.rockstargames.com/"
                    },
                    new Publisher()
                    {
                        UserId = 7,
                        CompanyName = "Electronic Arts",
                        Description = "Electronic Arts description...",
                        HomePage = "https://www.ea.com/"
                    }
                });
            }

            context.SaveChanges();

            if (!context.Games.Any())
            {
                PlatformType desktop = context.PlatformTypes
                .Single(g => g.Type == "Desktop");
                PlatformType console = context.PlatformTypes
                    .Single(g => g.Type == "Console");
                PlatformType browser = context.PlatformTypes
                    .Single(g => g.Type == "Browser");
                PlatformType mobile = context.PlatformTypes
                    .Single(g => g.Type == "Mobile");

                context.Games.AddRange(new List<Game>()
                {
                    new Game()
                    {
                        Name = "Warcraft III",
                        Key = "69bb25f3-16b0-4eec-8c27-39b54e67664d",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Strategy"),
                            context.Genres.Single(g => g.Name == "RTS"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Blizzard Entertainment"),
                        Price = 100,
                        UnitsInStock = 5,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Game()
                    {
                        Name = "Need for Speed",
                        Key = "8e6f8a01-ed53-4489-ab30-715e002a10aa",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Races"),
                            context.Genres.Single(g => g.Name == "Formula"),
                            context.Genres.Single(g => g.Name == "Off-road")
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Electronic Arts"),
                        Price = 50,
                        UnitsInStock = 30,
                        Discontinued = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-7)
                    },
                    new Game()
                    {
                        Name = "Barbie Land",
                        Key = "f94cd44f-4159-4df3-9f6c-6134b298295f",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Adventure"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            browser
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Rockstar"),
                        Price = 15,
                        UnitsInStock = 80,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddMonths(-2)
                    },
                    new Game()
                    {
                        Name = "Eternal Conquest",
                        Key = Guid.NewGuid().ToString(),
                        Description = "An epic RPG game set in a fantasy realm, where players will fight powerful foes and make vital alliances.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "RPG"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Blizzard Entertainment"),
                        Price = 80,
                        UnitsInStock = 25,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Game()
                    {
                        Name = "Track Titans",
                        Key = Guid.NewGuid().ToString(),
                        Description = "An adrenaline-filled racing game that puts players in the driver's seat of the world's fastest cars.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Races"),
                            context.Genres.Single(g => g.Name == "Rally"),
                            context.Genres.Single(g => g.Name == "Formula")
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Electronic Arts"),
                        Price = 60,
                        UnitsInStock = 35,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddDays(-14)
                    },
                    new Game()
                    {
                        Name = "Mystery Mansion",
                        Key = Guid.NewGuid().ToString(),
                        Description = "A puzzle game where players solve mysteries to escape a haunted mansion.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Puzzle&Skill"),
                            context.Genres.Single(g => g.Name == "Adventure")
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            browser
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Rockstar"),
                        Price = 20,
                        UnitsInStock = 40,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddDays(-21)
                    },
                    new Game()
                    {
                        Name = "Space Siege",
                        Key = Guid.NewGuid().ToString(),
                        Description = "A strategy game where players command a space fleet to defend against alien invasions.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Strategy"),
                            context.Genres.Single(g => g.Name == "RTS"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Blizzard Entertainment"),
                        Price = 70,
                        UnitsInStock = 30,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddDays(-28)
                    },
                    new Game()
                    {
                        Name = "Gladiator's Triumph",
                        Key = Guid.NewGuid().ToString(),
                        Description = "Action game where players become a legendary gladiator, fighting for glory in epic battles.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Action"),
                            context.Genres.Single(g => g.Name == "TPS"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Rockstar"),
                        Price = 50,
                        UnitsInStock = 20,
                        Discontinued = true,
                        CreatedAt = DateTime.UtcNow.AddMonths(-1)
                    },
                    new Game()
                    {
                        Name = "Viking's Raid",
                        Key = Guid.NewGuid().ToString(),
                        Description = "Adventure game set in the Viking era. Conquer new lands, build your clan, and make your legend.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Adventure"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Electronic Arts"),
                        Price = 65,
                        UnitsInStock = 10,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddMonths(-3)
                    },
                    new Game()
                    {
                        Name = "Football Fantasy",
                        Key = Guid.NewGuid().ToString(),
                        Description = "Become the greatest football manager in this exciting sports game. Manage your team and lead them to victory!",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Sports"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            mobile
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Electronic Arts"),
                        Price = 45,
                        UnitsInStock = 30,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddMonths(-2)
                    },
                    new Game()
                    {
                        Name = "Island Survival",
                        Key = Guid.NewGuid().ToString(),
                        Description = "Adventure survival game where players must gather resources, build shelters, and survive on a deserted island.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Adventure"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            mobile,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Rockstar"),
                        Price = 35,
                        UnitsInStock = 50,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddDays(-60)
                    },
                    new Game()
                    {
                        Name = "Candy Castle",
                        Key = Guid.NewGuid().ToString(),
                        Description = "A fun and colorful puzzle game where players match candies to complete the levels and progress.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Puzzle&Skill"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            browser,
                            mobile
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Blizzard Entertainment"),
                        Price = 10,
                        UnitsInStock = 100,
                        Discontinued = false,
                        CreatedAt = DateTime.UtcNow.AddMonths(-6)
                    },
                    new Game()
                    {
                        Name = "Cybernetic Showdown",
                        Key = Guid.NewGuid().ToString(),
                        Description = "In a future where machines rule, join the resistance in this action-packed FPS game.",
                        Genres = new List<Genre>
                        {
                            context.Genres.Single(g => g.Name == "Action"),
                            context.Genres.Single(g => g.Name == "FPS"),
                        },
                        PlatformTypes = new List<PlatformType>()
                        {
                            desktop,
                            console
                        },
                        Publisher = context.Publishers.Single(p => p.CompanyName == "Rockstar"),
                        Price = 55,
                        UnitsInStock = 20,
                        Discontinued = true,
                        CreatedAt = DateTime.UtcNow.AddMonths(-8)
                    }
                });
            }
        }
    }
}
