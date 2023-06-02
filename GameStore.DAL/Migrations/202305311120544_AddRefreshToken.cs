namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRefreshToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "RefreshToken", c => c.String());
            AddColumn("dbo.User", "RefreshTokenExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "RefreshTokenExpirationDate");
            DropColumn("dbo.User", "RefreshToken");
        }
    }
}
