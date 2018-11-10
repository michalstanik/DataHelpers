namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectFileEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        XMLContent = c.String(),
                        ProjectId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectFiles", "ProjectId", "dbo.Project");
            DropIndex("dbo.ProjectFiles", new[] { "ProjectId" });
            DropTable("dbo.ProjectFiles");
        }
    }
}
