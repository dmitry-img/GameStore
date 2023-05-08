namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublisherAndGameCommercialColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 40),
                        Description = c.String(),
                        HomePage = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Game", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Game", "UnitsInStock", c => c.Short(nullable: false));
            AddColumn("dbo.Game", "Discontinued", c => c.Boolean(nullable: false));
            AddColumn("dbo.Game", "PublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Game", "PublisherId");
            AddForeignKey("dbo.Game", "PublisherId", "dbo.Publisher", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game", "PublisherId", "dbo.Publisher");
            DropIndex("dbo.Game", new[] { "PublisherId" });
            DropColumn("dbo.Game", "PublisherId");
            DropColumn("dbo.Game", "Discontinued");
            DropColumn("dbo.Game", "UnitsInStock");
            DropColumn("dbo.Game", "Price");
            DropTable("dbo.Publisher");
        }
    }
}
