namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteParentChildCommentLogic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "ParentCommentId", "dbo.Comment");
            DropIndex("dbo.Comment", new[] { "ParentCommentId" });
            DropColumn("dbo.Comment", "ParentCommentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "ParentCommentId", c => c.Int());
            CreateIndex("dbo.Comment", "ParentCommentId");
            AddForeignKey("dbo.Comment", "ParentCommentId", "dbo.Comment", "Id");
        }
    }
}
