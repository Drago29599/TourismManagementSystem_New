namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketNoToPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "TicketNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "TicketNumber");
        }
    }
}
