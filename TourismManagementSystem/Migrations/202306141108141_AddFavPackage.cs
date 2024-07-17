namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavPackage : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.FavoritePackageId)
                .ForeignKey("dbo.Customers", t => t.customer_CustomerId)
                .ForeignKey("dbo.Packages", t => t.Package_PackId)
                .Index(t => t.customer_CustomerId)
                .Index(t => t.Package_PackId);
            
            CreateTable(
                "dbo.PackageCustomers",
                c => new
                    {
                        Package_PackId = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Package_PackId, t.Customer_CustomerId })
                .ForeignKey("dbo.Packages", t => t.Package_PackId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId, cascadeDelete: true)
                .Index(t => t.Package_PackId)
                .Index(t => t.Customer_CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoritePackages", "Package_PackId", "dbo.Packages");
            DropForeignKey("dbo.FavoritePackages", "customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PackageCustomers", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PackageCustomers", "Package_PackId", "dbo.Packages");
            DropIndex("dbo.PackageCustomers", new[] { "Customer_CustomerId" });
            DropIndex("dbo.PackageCustomers", new[] { "Package_PackId" });
            DropIndex("dbo.FavoritePackages", new[] { "Package_PackId" });
            DropIndex("dbo.FavoritePackages", new[] { "customer_CustomerId" });
            DropTable("dbo.PackageCustomers");
            DropTable("dbo.FavoritePackages");
        }
    }
}
