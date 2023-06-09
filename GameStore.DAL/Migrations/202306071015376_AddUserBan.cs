namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserBan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "BanEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "BanEndDate");
        }
    }
}
