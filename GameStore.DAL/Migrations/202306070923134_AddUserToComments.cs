namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "UserId");
            AddForeignKey("dbo.Comment", "UserId", "dbo.User", "Id", cascadeDelete: true);
            DropColumn("dbo.Comment", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "Name", c => c.String());
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropColumn("dbo.Comment", "UserId");
        }
    }
}
