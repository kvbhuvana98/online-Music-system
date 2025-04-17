namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewMusic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicCategories",
                c => new
                    {
                        MusicCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MusicCategoryId);
            
            CreateTable(
                "dbo.ViewMusics",
                c => new
                    {
                        ViewMusicId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Artist = c.String(),
                        MusicUrl = c.String(),
                        Category = c.String(),
                        MusicCategory_MusicCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ViewMusicId)
                .ForeignKey("dbo.MusicCategories", t => t.MusicCategory_MusicCategoryId)
                .Index(t => t.MusicCategory_MusicCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewMusics", "MusicCategory_MusicCategoryId", "dbo.MusicCategories");
            DropIndex("dbo.ViewMusics", new[] { "MusicCategory_MusicCategoryId" });
            DropTable("dbo.ViewMusics");
            DropTable("dbo.MusicCategories");
        }
    }
}
