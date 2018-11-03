namespace DataHelpers.Data.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAccountEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAccountName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "UserAccountId", c => c.Int());
            CreateIndex("dbo.User", "UserAccountId");
            AddForeignKey("dbo.User", "UserAccountId", "dbo.UserAccount", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserAccountId", "dbo.UserAccount");
            DropIndex("dbo.User", new[] { "UserAccountId" });
            DropColumn("dbo.User", "UserAccountId");
            DropTable("dbo.UserAccount");
        }
    }
}
