namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FIO = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Addr1 = c.String(maxLength: 4000),
                        Addr2 = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCarts", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "Id" });
            DropForeignKey("dbo.Orders", "Id", "dbo.ShoppingCarts");
            DropTable("dbo.Orders");
        }
    }
}
