namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaidFieldToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Paid");
        }
    }
}
