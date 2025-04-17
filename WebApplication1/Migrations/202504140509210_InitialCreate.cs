namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.MusicItems",
                c => new
                    {
                        MusicItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Artist = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MusicItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MusicItems");
            DropTable("dbo.Admins");
        }
    }
}
