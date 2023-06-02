namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullablePublisher : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Game", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.Game", new[] { "PublisherId" });
            AlterColumn("dbo.Game", "PublisherId", c => c.Int());
            CreateIndex("dbo.Game", "PublisherId");
            AddForeignKey("dbo.Game", "PublisherId", "dbo.Publisher", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.Game", new[] { "PublisherId" });
            AlterColumn("dbo.Game", "PublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Game", "PublisherId");
            AddForeignKey("dbo.Game", "PublisherId", "dbo.Publisher", "Id", cascadeDelete: true);
        }
    }
}
