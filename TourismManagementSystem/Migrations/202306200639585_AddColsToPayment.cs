namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColsToPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PackageName", c => c.String());
            AddColumn("dbo.Payments", "Guest1", c => c.String(nullable: false));
            AddColumn("dbo.Payments", "GuestContactNo", c => c.Long(nullable: false));
            AddColumn("dbo.Payments", "Guest2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Guest2");
            DropColumn("dbo.Payments", "GuestContactNo");
            DropColumn("dbo.Payments", "Guest1");
            DropColumn("dbo.Payments", "PackageName");
        }
    }
}
