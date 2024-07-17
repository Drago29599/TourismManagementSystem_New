namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsPurchaseCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "IsPurchased");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "IsPurchased", c => c.Boolean(nullable: false));
        }
    }
}
