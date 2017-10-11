namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        FIO = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Addr1 = c.String(maxLength: 4000),
                        Addr2 = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCarts", t => t.CartId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropForeignKey("dbo.Orders", "CartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserProfile");
            DropTable("dbo.Orders");
        }
    }
}
