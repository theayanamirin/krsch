namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDba : DbMigration
    {
        public override void Up()
        {
            
            
            AddColumn("dbo.Clients", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Clients", new[] { "Id" });
            AddPrimaryKey("dbo.Clients", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Clients", "UserId", "dbo.UserProfile", "UserId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Clients", "UserId");
            DropColumn("dbo.Clients", "Id");
            DropColumn("dbo.Clients", "Login");
            DropColumn("dbo.Comments", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Login", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.webpages_UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.webpages_UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropForeignKey("dbo.webpages_UsersInRoles", "RoleId", "dbo.webpages_Roles");
            DropForeignKey("dbo.webpages_UsersInRoles", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Clients", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Comments", "UserId", "dbo.UserProfile");
            DropPrimaryKey("dbo.Clients", new[] { "UserId" });
            AddPrimaryKey("dbo.Clients", "Id");
            DropColumn("dbo.Comments", "UserId");
            DropColumn("dbo.Clients", "UserId");
            DropTable("dbo.webpages_UsersInRoles");
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.UserProfile");
        }
    }
}
