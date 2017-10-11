namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserProfile");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AddColumn("dbo.Orders", "UserProfile_UserId", c => c.Int());
            AddForeignKey("dbo.Orders", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Orders", "UserProfile_UserId");
            DropColumn("dbo.Orders", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            DropIndex("dbo.Orders", new[] { "UserProfile_UserId" });
            DropForeignKey("dbo.Orders", "UserProfile_UserId", "dbo.UserProfile");
            DropColumn("dbo.Orders", "UserProfile_UserId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
