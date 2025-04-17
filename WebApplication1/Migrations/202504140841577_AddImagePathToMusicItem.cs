namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToMusicItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicItems", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicItems", "ImagePath");
        }
    }
}
