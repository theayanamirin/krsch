namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Orders", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.Orders", new[] { "ShoppingCartId" });
            DropIndex("dbo.Orders", new[] { "UserProfile_UserId" });
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoppingCartId = c.Int(nullable: false),
                        Shipped = c.Boolean(nullable: false),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "UserProfile_UserId");
            CreateIndex("dbo.Orders", "ShoppingCartId");
            AddForeignKey("dbo.Orders", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.Orders", "ShoppingCartId", "dbo.ShoppingCarts", "Id", cascadeDelete: true);
        }
    }
}
