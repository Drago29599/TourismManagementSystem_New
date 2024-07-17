namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgInPack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Image");
        }
    }
}
