namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToPublishers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publisher", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Publisher", "UserId");
            AddForeignKey("dbo.Publisher", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publisher", "UserId", "dbo.User");
            DropIndex("dbo.Publisher", new[] { "UserId" });
            DropColumn("dbo.Publisher", "UserId");
        }
    }
}
