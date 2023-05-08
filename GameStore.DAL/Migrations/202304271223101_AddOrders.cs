namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Game", "Price", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "GameId", "dbo.Game");
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.OrderDetail", new[] { "GameId" });
            AlterColumn("dbo.Game", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
        }
    }
}
