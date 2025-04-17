namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMusicCategoryIdToViewMusic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ViewMusics", "MusicCategory_MusicCategoryId", "dbo.MusicCategories");
            DropIndex("dbo.ViewMusics", new[] { "MusicCategory_MusicCategoryId" });
            RenameColumn(table: "dbo.ViewMusics", name: "MusicCategory_MusicCategoryId", newName: "MusicCategoryId");
            AlterColumn("dbo.ViewMusics", "MusicCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.ViewMusics", "MusicCategoryId");
            AddForeignKey("dbo.ViewMusics", "MusicCategoryId", "dbo.MusicCategories", "MusicCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewMusics", "MusicCategoryId", "dbo.MusicCategories");
            DropIndex("dbo.ViewMusics", new[] { "MusicCategoryId" });
            AlterColumn("dbo.ViewMusics", "MusicCategoryId", c => c.Int());
            RenameColumn(table: "dbo.ViewMusics", name: "MusicCategoryId", newName: "MusicCategory_MusicCategoryId");
            CreateIndex("dbo.ViewMusics", "MusicCategory_MusicCategoryId");
            AddForeignKey("dbo.ViewMusics", "MusicCategory_MusicCategoryId", "dbo.MusicCategories", "MusicCategoryId");
        }
    }
}
