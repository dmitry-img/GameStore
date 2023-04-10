namespace GameStore.DAL.Migrations
{
    using GameStore.DAL.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

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
        }
    }
}
