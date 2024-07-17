namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColAsUniqueInAdmin : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Admins", "AdminUserName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Admins", new[] { "AdminUserName" });
        }
    }
}
