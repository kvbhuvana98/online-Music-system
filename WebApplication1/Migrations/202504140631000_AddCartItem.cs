namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        MusicItemId = c.Int(nullable: false),
                        CartId = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.MusicItems", t => t.MusicItemId, cascadeDelete: true)
                .Index(t => t.MusicItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "MusicItemId", "dbo.MusicItems");
            DropIndex("dbo.CartItems", new[] { "MusicItemId" });
            DropTable("dbo.CartItems");
        }
    }
}
