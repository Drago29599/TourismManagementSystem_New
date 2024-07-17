namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourismDB5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "IsPurchased", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "IsPurchased");
        }
    }
}
