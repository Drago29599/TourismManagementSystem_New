namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "GuestPhoneNumber", c => c.String());
            DropColumn("dbo.Payments", "GuestContactNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "GuestContactNo", c => c.Long(nullable: false));
            DropColumn("dbo.Payments", "GuestPhoneNumber");
        }
    }
}
