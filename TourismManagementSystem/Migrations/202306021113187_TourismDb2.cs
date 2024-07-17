namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerPassword", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Admins", "AdminPassword", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdminPassword", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPassword", c => c.String());
        }
    }
}
