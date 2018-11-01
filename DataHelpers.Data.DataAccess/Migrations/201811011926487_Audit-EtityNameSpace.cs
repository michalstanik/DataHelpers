namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditEtityNameSpace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLog", "EntityNamespace", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditLog", "EntityNamespace");
        }
    }
}
