namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb : DbMigration
    {
        public override void Up()
        {
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
                        PackageId = c.String(nullable: false, maxLength: 8),
                        PackageName = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        PickupPoint = c.String(nullable: false, maxLength: 100),
                        Customer_CustomerId = c.Int(),
                        Package_PackageId = c.String(maxLength: 8),
                        Admin_AdminId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_PackageId)
                .ForeignKey("dbo.Admins", t => t.Admin_AdminId)
                .Index(t => t.CustomerId)
                .Index(t => t.PackageId)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Package_PackageId)
                .Index(t => t.Admin_AdminId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        CustomerEmailId = c.String(),
                        CustomerPassword = c.String(),
                        CustomerPhone = c.Long(nullable: false),
                        CustomerAdress = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.String(nullable: false, maxLength: 8),
                        PackageName = c.String(nullable: false, maxLength: 100),
                        Price = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PickupPoint = c.String(nullable: false, maxLength: 100),
                        PackageDetails = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.PackageId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminName = c.String(),
                        AdminPassword = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Admin_AdminId", "dbo.Admins");
            DropForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "Admin_AdminId" });
            DropIndex("dbo.Bookings", new[] { "Package_PackageId" });
            DropIndex("dbo.Bookings", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Bookings", new[] { "PackageId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.Admins");
            DropTable("dbo.Packages");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
