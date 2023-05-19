namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentQuotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "HasQuote", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "HasQuote");
        }
    }
}
