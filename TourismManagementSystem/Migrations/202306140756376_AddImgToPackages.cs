namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgToPackages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "PackageImageFilename", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "PackageImageFilename");
        }
    }
}
