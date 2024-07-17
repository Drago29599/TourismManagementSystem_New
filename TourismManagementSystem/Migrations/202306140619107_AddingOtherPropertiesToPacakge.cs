namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOtherPropertiesToPacakge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Country", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Packages", "State", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Packages", "Destination", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Packages", "PackageDetails", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Packages", "PackageDetails", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Packages", "Destination");
            DropColumn("dbo.Packages", "State");
            DropColumn("dbo.Packages", "Country");
        }
    }
}
