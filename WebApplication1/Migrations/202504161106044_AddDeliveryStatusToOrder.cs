namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryStatusToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryStatus");
        }
    }
}
