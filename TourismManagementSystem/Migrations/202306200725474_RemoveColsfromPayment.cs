namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColsfromPayment : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payments", "Guest1");
            DropColumn("dbo.Payments", "Guest2");
            DropColumn("dbo.Payments", "GuestPhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "GuestPhoneNumber", c => c.String());
            AddColumn("dbo.Payments", "Guest2", c => c.String());
            AddColumn("dbo.Payments", "Guest1", c => c.String(nullable: false));
        }
    }
}
