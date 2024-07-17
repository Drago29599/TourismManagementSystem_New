namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewIdToPackage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages");
            DropIndex("dbo.Bookings", new[] { "PackageId" });
            DropIndex("dbo.Bookings", new[] { "Package_PackageId" });
            RenameColumn(table: "dbo.Bookings", name: "Package_PackageId", newName: "Package_PackId");
            RenameColumn(table: "dbo.Bookings", name: "PackageId", newName: "PackId");
            DropPrimaryKey("dbo.Packages");
            AddColumn("dbo.Packages", "PackId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Bookings", "PackId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "Package_PackId", c => c.Int());
            AddPrimaryKey("dbo.Packages", "PackId");
            CreateIndex("dbo.Bookings", "PackId");
            CreateIndex("dbo.Bookings", "Package_PackId");
            AddForeignKey("dbo.Bookings", "PackId", "dbo.Packages", "PackId", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "Package_PackId", "dbo.Packages", "PackId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Package_PackId", "dbo.Packages");
            DropForeignKey("dbo.Bookings", "PackId", "dbo.Packages");
            DropIndex("dbo.Bookings", new[] { "Package_PackId" });
            DropIndex("dbo.Bookings", new[] { "PackId" });
            DropPrimaryKey("dbo.Packages");
            AlterColumn("dbo.Bookings", "Package_PackId", c => c.String(maxLength: 50));
            AlterColumn("dbo.Bookings", "PackId", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Packages", "PackId");
            AddPrimaryKey("dbo.Packages", "PackageId");
            RenameColumn(table: "dbo.Bookings", name: "PackId", newName: "PackageId");
            RenameColumn(table: "dbo.Bookings", name: "Package_PackId", newName: "Package_PackageId");
            CreateIndex("dbo.Bookings", "Package_PackageId");
            CreateIndex("dbo.Bookings", "PackageId");
            AddForeignKey("dbo.Bookings", "Package_PackageId", "dbo.Packages", "PackageId");
            AddForeignKey("dbo.Bookings", "PackageId", "dbo.Packages", "PackageId", cascadeDelete: true);
        }
    }
}
