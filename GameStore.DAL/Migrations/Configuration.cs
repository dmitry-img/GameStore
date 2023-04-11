namespace GameStore.DAL.Migrations
{
    using GameStore.DAL.Entities;
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
            context.Genres.AddRange(new List<Genre>()
            {
                new Genre() 
                { 
                    Name = "Strategy", 
                    ChildGenres = new List<Genre>()
                    {
                        new Genre() { Name = "RTS"},
                        new Genre() { Name = "TBS"},
                    }
                },
                new Genre() { Name = "RPG"},
                new Genre() { Name = "Sports" },
                new Genre() 
                { 
                    Name = "Races",
                    ChildGenres = new List<Genre>()
                    {
                        new Genre() { Name = "Rally"},
                        new Genre() { Name = "Arcade"},
                        new Genre() { Name = "Formula"},
                        new Genre() { Name = "Off-road"},
                    }
                },
                new Genre() 
                { 
                    Name = "Action",
                    ChildGenres = new List<Genre>()
                    {
                        new Genre() { Name = "FPS"},
                        new Genre() { Name = "TPS"}
                    }
                },
                new Genre() { Name = "Adventure"},
                new Genre() { Name = "Puzzle&Skill"},
                new Genre() { Name = "Misc"},
            });

            context.PlatformTypes.AddRange(new List<PlatformType>()
            {
                new PlatformType() { Type = "Mobile" },
                new PlatformType() { Type = "Browser" },
                new PlatformType() { Type = "Desktop" },
                new PlatformType() { Type = "Console" },
            });

            context.SaveChanges();

            PlatformType desktop = context.PlatformTypes
                .Single(g => g.Type == "Desktop");
            PlatformType console = context.PlatformTypes
                .Single(g => g.Type == "Console");
            PlatformType browser = context.PlatformTypes
                .Single(g => g.Type == "Browser");

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
                    }
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
                    }
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
                    }
                }
            });
        }
    }
}
