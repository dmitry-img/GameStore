namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        GameId = c.Int(nullable: false),
                        ParentCommentId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.ParentCommentId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.ParentCommentId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameGenre",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.GenreId })
                .ForeignKey("dbo.Genre", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentGenreId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.ParentGenreId)
                .Index(t => t.ParentGenreId);
            
            CreateTable(
                "dbo.GamePlatformType",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlatformTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlatformTypeId })
                .ForeignKey("dbo.PlatformType", t => t.PlatformTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.PlatformTypeId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.PlatformType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GamePlatformType", "GameId", "dbo.Game");
            DropForeignKey("dbo.GamePlatformType", "PlatformTypeId", "dbo.PlatformType");
            DropForeignKey("dbo.GameGenre", "GameId", "dbo.Game");
            DropForeignKey("dbo.GameGenre", "GameId", "dbo.Genre");
            DropForeignKey("dbo.Genre", "ParentGenreId", "dbo.Genre");
            DropForeignKey("dbo.Comment", "GameId", "dbo.Game");
            DropForeignKey("dbo.Comment", "ParentCommentId", "dbo.Comment");
            DropIndex("dbo.GamePlatformType", new[] { "GameId" });
            DropIndex("dbo.GamePlatformType", new[] { "PlatformTypeId" });
            DropIndex("dbo.GameGenre", new[] { "GameId" });
            DropIndex("dbo.GameGenre", new[] { "GameId" });
            DropIndex("dbo.Genre", new[] { "ParentGenreId" });
            DropIndex("dbo.Comment", new[] { "GameId" });
            DropIndex("dbo.Comment", new[] { "ParentCommentId" });
            DropTable("dbo.PlatformType");
            DropTable("dbo.GamePlatformType");
            DropTable("dbo.Genre");
            DropTable("dbo.GameGenre");
            DropTable("dbo.Game");
            DropTable("dbo.Comment");
        }
    }
}
