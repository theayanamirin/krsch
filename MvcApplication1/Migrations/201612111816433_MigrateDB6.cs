namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckedOut = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        
                    })
                .PrimaryKey(t => new { t.ItemId, t.CartId })
                .ForeignKey("dbo.Books", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.CartId, cascadeDelete: true)
               
                .Index(t => t.ItemId)
                .Index(t => t.CartId)
                ;
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoppingCartId = c.Int(nullable: false),
                        Shipped = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ShoppingCartId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "ShoppingCartId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "UserProfile_UserId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.CartItems", new[] { "ItemId" });
            DropIndex("dbo.ShoppingCarts", new[] { "UserId" });
            DropForeignKey("dbo.Orders", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.CartItems", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.CartItems", "ItemId", "dbo.Books");
            DropForeignKey("dbo.ShoppingCarts", "UserId", "dbo.UserProfile");
            DropTable("dbo.Orders");
            DropTable("dbo.CartItems");
            DropTable("dbo.ShoppingCarts");
        }
    }
}
