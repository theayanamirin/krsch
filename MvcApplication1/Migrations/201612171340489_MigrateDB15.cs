namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Orders", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Orders", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserProfile");
            DropColumn("dbo.Orders", "UserId");
        }
    }
}
