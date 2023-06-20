namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedRolesAndOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            AddColumn("dbo.Order", "ShippedDate", c => c.DateTime());
            AddColumn("dbo.Order", "OrderState", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "CustomerId", c => c.String());
            AlterColumn("dbo.User", "RoleId", c => c.Int());
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Role", "Id");
            DropColumn("dbo.Order", "Paid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Paid", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            AlterColumn("dbo.User", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Order", "OrderState");
            DropColumn("dbo.Order", "ShippedDate");
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Role", "Id", cascadeDelete: true);
        }
    }
}
