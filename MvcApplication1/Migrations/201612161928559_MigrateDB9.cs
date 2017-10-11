namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ItemId", "dbo.Books");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.CartItems", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.CartItems", new[] { "ItemId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.CartItems", new[] { "UserProfile_UserId" });
            AddColumn("dbo.News", "Added", c => c.DateTime(nullable: false));
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => new { t.ItemId, t.CartId });
            
            DropColumn("dbo.News", "Added");
            CreateIndex("dbo.CartItems", "UserProfile_UserId");
            CreateIndex("dbo.CartItems", "CartId");
            CreateIndex("dbo.CartItems", "ItemId");
            AddForeignKey("dbo.CartItems", "UserProfile_UserId", "dbo.UserProfile", "UserId");
            AddForeignKey("dbo.CartItems", "CartId", "dbo.ShoppingCarts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CartItems", "ItemId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
