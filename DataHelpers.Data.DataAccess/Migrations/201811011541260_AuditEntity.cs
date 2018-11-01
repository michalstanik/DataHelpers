namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        FieldName = c.String(),
                        ChangeTime = c.DateTime(nullable: false),
                        PreviousValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLog");
        }
    }
}
