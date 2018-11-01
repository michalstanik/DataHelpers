namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditEntityId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLog", "EntityId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditLog", "EntityId");
        }
    }
}
