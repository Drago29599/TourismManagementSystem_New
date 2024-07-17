namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "PackId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "Package_PackId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "Admin_AdminId", "dbo.Admins");
            DropForeignKey("dbo.Bookings", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.FavoritePackages", "customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FavoritePackages", "Package_PackId", "dbo.Packages");
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.Bookings", new[] { "PackId" });
            DropIndex("dbo.Bookings", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Bookings", new[] { "Package_PackId" });
            DropIndex("dbo.Bookings", new[] { "Admin_AdminId" });
            DropIndex("dbo.Bookings", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.FavoritePackages", new[] { "customer_CustomerId" });
            DropIndex("dbo.FavoritePackages", new[] { "Package_PackId" });
            AddColumn("dbo.Payments", "Employee_EmployeeId", c => c.Int());
            AddColumn("dbo.Payments", "Admin_AdminId", c => c.Int());
            CreateIndex("dbo.Payments", "CustomerId");
            CreateIndex("dbo.Payments", "PackageId");
            CreateIndex("dbo.Payments", "Employee_EmployeeId");
            CreateIndex("dbo.Payments", "Admin_AdminId");
            AddForeignKey("dbo.Payments", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Payments", "Admin_AdminId", "dbo.Admins", "AdminId");
            AddForeignKey("dbo.Payments", "PackageId", "dbo.Packages", "PackId", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            DropTable("dbo.Bookings");
            DropTable("dbo.FavoritePackages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FavoritePackages",
                c => new
                    {
                        FavoritePackageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        customer_CustomerId = c.Int(),
                        Package_PackId = c.Int(),
                    })
                .PrimaryKey(t => t.FavoritePackageId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionAmount = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        CustomerPhone = c.Long(nullable: false),
                        CustomerEmailId = c.String(nullable: false, maxLength: 100),
                        PackId = c.Int(nullable: false),
                        PackageName = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        PickupPoint = c.String(nullable: false, maxLength: 100),
                        Customer_CustomerId = c.Int(),
                        Package_PackId = c.Int(),
                        Admin_AdminId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Payments", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Payments", "Admin_AdminId", "dbo.Admins");
            DropForeignKey("dbo.Payments", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Payments", new[] { "Admin_AdminId" });
            DropIndex("dbo.Payments", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Payments", new[] { "PackageId" });
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropColumn("dbo.Payments", "Admin_AdminId");
            DropColumn("dbo.Payments", "Employee_EmployeeId");
            CreateIndex("dbo.FavoritePackages", "Package_PackId");
            CreateIndex("dbo.FavoritePackages", "customer_CustomerId");
            CreateIndex("dbo.Bookings", "Employee_EmployeeId");
            CreateIndex("dbo.Bookings", "Admin_AdminId");
            CreateIndex("dbo.Bookings", "Package_PackId");
            CreateIndex("dbo.Bookings", "Customer_CustomerId");
            CreateIndex("dbo.Bookings", "PackId");
            CreateIndex("dbo.Bookings", "CustomerId");
            AddForeignKey("dbo.FavoritePackages", "Package_PackId", "dbo.Packages", "PackId");
            AddForeignKey("dbo.FavoritePackages", "customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Bookings", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Bookings", "Admin_AdminId", "dbo.Admins", "AdminId");
            AddForeignKey("dbo.Bookings", "Package_PackId", "dbo.Packages", "PackId");
            AddForeignKey("dbo.Bookings", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Bookings", "PackId", "dbo.Packages", "PackId", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
