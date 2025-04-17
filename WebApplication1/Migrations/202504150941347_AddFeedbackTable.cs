namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeedbackTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Message = c.String(nullable: false, maxLength: 1000),
                        SubmittedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feedbacks");
        }
    }
}
