namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableUserInPublisher : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Publisher", "UserId", "dbo.User");
            DropIndex("dbo.Publisher", new[] { "UserId" });
            AlterColumn("dbo.Publisher", "UserId", c => c.Int());
            CreateIndex("dbo.Publisher", "UserId");
            AddForeignKey("dbo.Publisher", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publisher", "UserId", "dbo.User");
            DropIndex("dbo.Publisher", new[] { "UserId" });
            AlterColumn("dbo.Publisher", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Publisher", "UserId");
            AddForeignKey("dbo.Publisher", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
