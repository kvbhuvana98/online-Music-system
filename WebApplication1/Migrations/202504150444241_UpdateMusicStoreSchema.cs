namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMusicStoreSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        MusicItemId = c.Int(nullable: false),
                        VotedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.MusicItems", t => t.MusicItemId, cascadeDelete: true)
                .Index(t => t.MusicItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "MusicItemId", "dbo.MusicItems");
            DropIndex("dbo.Votes", new[] { "MusicItemId" });
            DropTable("dbo.Votes");
        }
    }
}
