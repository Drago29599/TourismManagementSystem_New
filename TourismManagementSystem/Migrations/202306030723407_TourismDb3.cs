namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmpUserName = c.String(nullable: false, maxLength: 50),
                        EmployeePass = c.String(nullable: false, maxLength: 50),
                        EmployeeRole = c.String(nullable: false, maxLength: 100),
                        EmpFirstName = c.String(nullable: false, maxLength: 100),
                        EmpLastName = c.String(nullable: false, maxLength: 100),
                        EmpEmail = c.String(),
                        EmpPhoneNumber = c.Long(nullable: false),
                        EmpResidentialAddress = c.String(nullable: false, maxLength: 500),
                        Admin_AdminId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Admins", t => t.Admin_AdminId)
                .Index(t => t.Admin_AdminId);
            
            AddColumn("dbo.Admins", "AdminUserName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Admins", "AdminDesignation", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Bookings", "Employee_EmployeeId", c => c.Int());
            AlterColumn("dbo.Admins", "AdminName", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Bookings", "Employee_EmployeeId");
            AddForeignKey("dbo.Bookings", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Admin_AdminId", "dbo.Admins");
            DropForeignKey("dbo.Bookings", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "Admin_AdminId" });
            DropIndex("dbo.Bookings", new[] { "Employee_EmployeeId" });
            AlterColumn("dbo.Admins", "AdminName", c => c.String());
            DropColumn("dbo.Bookings", "Employee_EmployeeId");
            DropColumn("dbo.Admins", "AdminDesignation");
            DropColumn("dbo.Admins", "AdminUserName");
            DropTable("dbo.Employees");
        }
    }
}
