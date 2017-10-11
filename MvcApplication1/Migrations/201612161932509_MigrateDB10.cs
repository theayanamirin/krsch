namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB10 : DbMigration
    {
        public override void Up()
        {
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
                .Index(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.CartItems", new[] { "ItemId" });
            DropForeignKey("dbo.CartItems", "CartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.CartItems", "ItemId", "dbo.Books");
            DropTable("dbo.CartItems");
        }
    }
}
