namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectComponent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentName = c.String(nullable: false, maxLength: 50),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        ProjectDescription = c.String(),
                        ProjectTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectType", t => t.ProjectTypeId)
                .Index(t => t.ProjectTypeId);
            
            CreateTable(
                "dbo.ProjectType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectWorkspace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkspaceName = c.String(),
                        WorkspacePath = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectWorkspace", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "ProjectTypeId", "dbo.ProjectType");
            DropForeignKey("dbo.ProjectComponent", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProjectWorkspace", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "ProjectTypeId" });
            DropIndex("dbo.ProjectComponent", new[] { "ProjectId" });
            DropTable("dbo.ProjectWorkspace");
            DropTable("dbo.ProjectType");
            DropTable("dbo.Project");
            DropTable("dbo.ProjectComponent");
        }
    }
}
