namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Orders", "CartId", "dbo.ShoppingCarts");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropTable("dbo.Orders");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "CartId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "CartId", "dbo.ShoppingCarts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
