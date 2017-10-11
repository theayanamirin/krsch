namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreTitle = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.GenreId);
            
            AddColumn("dbo.Books", "GenreId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Books", "GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
            CreateIndex("dbo.Books", "GenreId");
            DropColumn("dbo.Books", "Genre_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Genre_id", c => c.Int(nullable: false));
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropColumn("dbo.Books", "GenreId");
            DropTable("dbo.Genres");
        }
    }
}
