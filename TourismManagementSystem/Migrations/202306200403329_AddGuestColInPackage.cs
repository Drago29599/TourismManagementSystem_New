namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuestColInPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "NoOfPeoples", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "NoOfPeoples");
        }
    }
}
