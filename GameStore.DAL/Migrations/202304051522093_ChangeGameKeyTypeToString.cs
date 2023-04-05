namespace GameStore.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGameKeyTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Game", "Key", c => c.String(nullable: false, maxLength: 255, unicode: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Game_Key_Unique",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Game_Key_Unique, IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.Genre", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Unique_Genre_Name",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Unique_Genre_Name, IsUnique: True }", newValue: null)
                    },
                }));
            AlterColumn("dbo.PlatformType", "Type", c => c.String(nullable: false, maxLength: 255, unicode: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Unique_Platform_Type",
                        new AnnotationValues(oldValue: "IndexAnnotation: { Name: Unique_Platform_Type, IsUnique: True }", newValue: null)
                    },
                }));
            CreateIndex("dbo.Game", "Key", unique: true, name: "Index");
            CreateIndex("dbo.Genre", "Name", unique: true, name: "Index");
            CreateIndex("dbo.PlatformType", "Type", unique: true, name: "Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlatformType", "Index");
            DropIndex("dbo.Genre", "Index");
            DropIndex("dbo.Game", "Index");
            AlterColumn("dbo.PlatformType", "Type", c => c.String(
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Unique_Platform_Type",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Unique_Platform_Type, IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Genre", "Name", c => c.String(
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Unique_Genre_Name",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Unique_Genre_Name, IsUnique: True }")
                    },
                }));
            AlterColumn("dbo.Game", "Key", c => c.Guid(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "Game_Key_Unique",
                        new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Game_Key_Unique, IsUnique: True }")
                    },
                }));
        }
    }
}
