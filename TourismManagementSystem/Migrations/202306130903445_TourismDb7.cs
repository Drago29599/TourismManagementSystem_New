namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerEmailId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerEmailId", c => c.String());
        }
    }
}
