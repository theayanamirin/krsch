namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Sent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "DateSent", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateSent");
            DropColumn("dbo.Orders", "Sent");
        }
    }
}
