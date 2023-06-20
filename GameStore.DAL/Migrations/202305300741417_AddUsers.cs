namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObjectId = c.String(),
                        Username = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedAt = c.DateTime(),
                        ModifiedBy = c.String(),
                        DeletedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "Username" });
            DropIndex("dbo.Role", new[] { "Name" });
            DropTable("dbo.User");
            DropTable("dbo.Role");
        }
    }
}
