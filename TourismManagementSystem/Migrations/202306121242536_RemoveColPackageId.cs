namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColPackageId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "PackageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "PackageId", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
