namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmpEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmpEmail", c => c.String());
        }
    }
}
