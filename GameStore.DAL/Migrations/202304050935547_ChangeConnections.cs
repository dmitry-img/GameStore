namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;    
    public partial class ChangeConnections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameGenre", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.GameGenre", "GameId", "dbo.Game");
            DropForeignKey("dbo.GamePlatformType", "PlatformTypeId", "dbo.PlatformType");
            DropForeignKey("dbo.GamePlatformType", "GameId", "dbo.Game");
            DropIndex("dbo.GameGenre", new[] { "GameId" });
            DropIndex("dbo.GameGenre", new[] { "GenreId" });
            DropIndex("dbo.GamePlatformType", new[] { "GameId" });
            DropIndex("dbo.GamePlatformType", new[] { "PlatformTypeId" });
            DropTable("dbo.GameGenre");
            DropTable("dbo.GamePlatformType");
            CreateTable(
                "dbo.GameGenre",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.Genre_Id })
                .ForeignKey("dbo.Game", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.GamePlatformType",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        PlatformType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.PlatformType_Id })
                .ForeignKey("dbo.Game", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.PlatformType", t => t.PlatformType_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.PlatformType_Id);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GamePlatformType",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlatformTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlatformTypeId });
            
            CreateTable(
                "dbo.GameGenre",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.GenreId });
            
            DropForeignKey("dbo.GamePlatformType", "PlatformType_Id", "dbo.PlatformType");
            DropForeignKey("dbo.GamePlatformType", "Game_Id", "dbo.Game");
            DropForeignKey("dbo.GameGenre", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.GameGenre", "Game_Id", "dbo.Game");
            DropIndex("dbo.GamePlatformType", new[] { "PlatformType_Id" });
            DropIndex("dbo.GamePlatformType", new[] { "Game_Id" });
            DropIndex("dbo.GameGenre", new[] { "Genre_Id" });
            DropIndex("dbo.GameGenre", new[] { "Game_Id" });
            DropTable("dbo.GamePlatformType");
            DropTable("dbo.GameGenre");
            CreateIndex("dbo.GamePlatformType", "PlatformTypeId");
            CreateIndex("dbo.GamePlatformType", "GameId");
            CreateIndex("dbo.GameGenre", "GenreId");
            CreateIndex("dbo.GameGenre", "GameId");
            AddForeignKey("dbo.GamePlatformType", "GameId", "dbo.Game", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GamePlatformType", "PlatformTypeId", "dbo.PlatformType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameGenre", "GameId", "dbo.Game", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameGenre", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
        }
    }
}
