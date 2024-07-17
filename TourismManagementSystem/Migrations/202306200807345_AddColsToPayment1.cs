namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColsToPayment1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "CustomerPhoneNumber", c => c.Long(nullable: false));
            AddColumn("dbo.Payments", "NoOfGuests", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Payments", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Payments", "PickupPoint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PickupPoint");
            DropColumn("dbo.Payments", "EndDate");
            DropColumn("dbo.Payments", "StartDate");
            DropColumn("dbo.Payments", "NoOfGuests");
            DropColumn("dbo.Payments", "CustomerPhoneNumber");
        }
    }
}
