namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDb4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages");
            DropIndex("dbo.Bookings", new[] { "PackageId" });
            DropIndex("dbo.Bookings", new[] { "Package_PackageId" });
            DropPrimaryKey("dbo.Packages");
            AlterColumn("dbo.Bookings", "PackageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "Package_PackageId", c => c.Int());
            AlterColumn("dbo.Packages", "PackageId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Packages", "PackageId");
            CreateIndex("dbo.Bookings", "PackageId");
            CreateIndex("dbo.Bookings", "Package_PackageId");
            AddForeignKey("dbo.Bookings", "PackageId", "dbo.Packages", "PackageId", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages", "PackageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "PackageId", "dbo.Packages");
            DropIndex("dbo.Bookings", new[] { "Package_PackageId" });
            DropIndex("dbo.Bookings", new[] { "PackageId" });
            DropPrimaryKey("dbo.Packages");
            AlterColumn("dbo.Packages", "PackageId", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Bookings", "Package_PackageId", c => c.String(maxLength: 8));
            AlterColumn("dbo.Bookings", "PackageId", c => c.String(nullable: false, maxLength: 8));
            AddPrimaryKey("dbo.Packages", "PackageId");
            CreateIndex("dbo.Bookings", "Package_PackageId");
            CreateIndex("dbo.Bookings", "PackageId");
            AddForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages", "PackageId");
            AddForeignKey("dbo.Bookings", "PackageId", "dbo.Packages", "PackageId", cascadeDelete: true);
        }
    }
}
