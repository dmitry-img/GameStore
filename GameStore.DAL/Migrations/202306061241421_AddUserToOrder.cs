namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "UserId");
            AddForeignKey("dbo.Order", "UserId", "dbo.User", "Id", cascadeDelete: true);
            DropColumn("dbo.Order", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CustomerId", c => c.String());
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropIndex("dbo.Order", new[] { "UserId" });
            DropColumn("dbo.Order", "UserId");
        }
    }
}
