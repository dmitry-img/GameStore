namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditableEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Comment", "CreatedBy", c => c.String());
            AddColumn("dbo.Comment", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.Comment", "ModifiedBy", c => c.String());
            AddColumn("dbo.Comment", "DeteledBy", c => c.String());
            AddColumn("dbo.Game", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Game", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Game", "CreatedBy", c => c.String());
            AddColumn("dbo.Game", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.Game", "ModifiedBy", c => c.String());
            AddColumn("dbo.Game", "DeteledBy", c => c.String());
            AddColumn("dbo.Genre", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Genre", "CreatedBy", c => c.String());
            AddColumn("dbo.Genre", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.Genre", "ModifiedBy", c => c.String());
            AddColumn("dbo.Genre", "DeteledBy", c => c.String());
            AddColumn("dbo.PlatformType", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.PlatformType", "CreatedBy", c => c.String());
            AddColumn("dbo.PlatformType", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.PlatformType", "ModifiedBy", c => c.String());
            AddColumn("dbo.PlatformType", "DeteledBy", c => c.String());
            AddColumn("dbo.Publisher", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Publisher", "CreatedBy", c => c.String());
            AddColumn("dbo.Publisher", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.Publisher", "ModifiedBy", c => c.String());
            AddColumn("dbo.Publisher", "DeteledBy", c => c.String());
            AddColumn("dbo.OrderDetail", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.OrderDetail", "CreatedBy", c => c.String());
            AddColumn("dbo.OrderDetail", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.OrderDetail", "ModifiedBy", c => c.String());
            AddColumn("dbo.OrderDetail", "DeteledBy", c => c.String());
            AddColumn("dbo.Order", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Order", "CreatedBy", c => c.String());
            AddColumn("dbo.Order", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.Order", "ModifiedBy", c => c.String());
            AddColumn("dbo.Order", "DeteledBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "DeteledBy");
            DropColumn("dbo.Order", "ModifiedBy");
            DropColumn("dbo.Order", "ModifiedAt");
            DropColumn("dbo.Order", "CreatedBy");
            DropColumn("dbo.Order", "CreatedAt");
            DropColumn("dbo.OrderDetail", "DeteledBy");
            DropColumn("dbo.OrderDetail", "ModifiedBy");
            DropColumn("dbo.OrderDetail", "ModifiedAt");
            DropColumn("dbo.OrderDetail", "CreatedBy");
            DropColumn("dbo.OrderDetail", "CreatedAt");
            DropColumn("dbo.Publisher", "DeteledBy");
            DropColumn("dbo.Publisher", "ModifiedBy");
            DropColumn("dbo.Publisher", "ModifiedAt");
            DropColumn("dbo.Publisher", "CreatedBy");
            DropColumn("dbo.Publisher", "CreatedAt");
            DropColumn("dbo.PlatformType", "DeteledBy");
            DropColumn("dbo.PlatformType", "ModifiedBy");
            DropColumn("dbo.PlatformType", "ModifiedAt");
            DropColumn("dbo.PlatformType", "CreatedBy");
            DropColumn("dbo.PlatformType", "CreatedAt");
            DropColumn("dbo.Genre", "DeteledBy");
            DropColumn("dbo.Genre", "ModifiedBy");
            DropColumn("dbo.Genre", "ModifiedAt");
            DropColumn("dbo.Genre", "CreatedBy");
            DropColumn("dbo.Genre", "CreatedAt");
            DropColumn("dbo.Game", "DeteledBy");
            DropColumn("dbo.Game", "ModifiedBy");
            DropColumn("dbo.Game", "ModifiedAt");
            DropColumn("dbo.Game", "CreatedBy");
            DropColumn("dbo.Game", "CreatedAt");
            DropColumn("dbo.Game", "Views");
            DropColumn("dbo.Comment", "DeteledBy");
            DropColumn("dbo.Comment", "ModifiedBy");
            DropColumn("dbo.Comment", "ModifiedAt");
            DropColumn("dbo.Comment", "CreatedBy");
            DropColumn("dbo.Comment", "CreatedAt");
        }
    }
}
