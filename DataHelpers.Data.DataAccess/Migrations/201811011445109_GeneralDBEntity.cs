namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneralDBEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectComponent", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectComponent", "ModifiedTime", c => c.DateTime());
            AddColumn("dbo.Project", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "ModifiedTime", c => c.DateTime());
            AddColumn("dbo.ProjectType", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectType", "ModifiedTime", c => c.DateTime());
            AddColumn("dbo.ProjectWorkspace", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectWorkspace", "ModifiedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectWorkspace", "ModifiedTime");
            DropColumn("dbo.ProjectWorkspace", "CreationTime");
            DropColumn("dbo.ProjectType", "ModifiedTime");
            DropColumn("dbo.ProjectType", "CreationTime");
            DropColumn("dbo.Project", "ModifiedTime");
            DropColumn("dbo.Project", "CreationTime");
            DropColumn("dbo.ProjectComponent", "ModifiedTime");
            DropColumn("dbo.ProjectComponent", "CreationTime");
        }
    }
}
