namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImgFromPack : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "PackageImageFilename");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "PackageImageFilename", c => c.String(nullable: false));
        }
    }
}
