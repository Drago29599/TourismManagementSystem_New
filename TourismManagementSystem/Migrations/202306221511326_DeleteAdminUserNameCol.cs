namespace TourismManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAdminUserNameCol : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Admins", new[] { "AdminUserName" });
            CreateIndex("dbo.Admins", "AdminName", unique: true);
            DropColumn("dbo.Admins", "AdminUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "AdminUserName", c => c.String(nullable: false, maxLength: 50));
            DropIndex("dbo.Admins", new[] { "AdminName" });
            CreateIndex("dbo.Admins", "AdminUserName", unique: true);
        }
    }
}
