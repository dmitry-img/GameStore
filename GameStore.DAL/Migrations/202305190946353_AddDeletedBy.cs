namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "DeletedBy", c => c.String());
            AddColumn("dbo.Game", "DeletedBy", c => c.String());
            AddColumn("dbo.Genre", "DeletedBy", c => c.String());
            AddColumn("dbo.PlatformType", "DeletedBy", c => c.String());
            AddColumn("dbo.Publisher", "DeletedBy", c => c.String());
            AddColumn("dbo.OrderDetail", "DeletedBy", c => c.String());
            AddColumn("dbo.Order", "DeletedBy", c => c.String());
            DropColumn("dbo.Comment", "DeteledBy");
            DropColumn("dbo.Game", "DeteledBy");
            DropColumn("dbo.Genre", "DeteledBy");
            DropColumn("dbo.PlatformType", "DeteledBy");
            DropColumn("dbo.Publisher", "DeteledBy");
            DropColumn("dbo.OrderDetail", "DeteledBy");
            DropColumn("dbo.Order", "DeteledBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "DeteledBy", c => c.String());
            AddColumn("dbo.OrderDetail", "DeteledBy", c => c.String());
            AddColumn("dbo.Publisher", "DeteledBy", c => c.String());
            AddColumn("dbo.PlatformType", "DeteledBy", c => c.String());
            AddColumn("dbo.Genre", "DeteledBy", c => c.String());
            AddColumn("dbo.Game", "DeteledBy", c => c.String());
            AddColumn("dbo.Comment", "DeteledBy", c => c.String());
            DropColumn("dbo.Order", "DeletedBy");
            DropColumn("dbo.OrderDetail", "DeletedBy");
            DropColumn("dbo.Publisher", "DeletedBy");
            DropColumn("dbo.PlatformType", "DeletedBy");
            DropColumn("dbo.Genre", "DeletedBy");
            DropColumn("dbo.Game", "DeletedBy");
            DropColumn("dbo.Comment", "DeletedBy");
        }
    }
}
