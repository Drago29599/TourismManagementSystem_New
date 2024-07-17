namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColToPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "GuestNames", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "GuestNames");
        }
    }
}
