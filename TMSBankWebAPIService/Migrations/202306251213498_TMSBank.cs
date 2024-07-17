namespace TMSBankWebAPIService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMSBank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "CardNumber", c => c.Long(nullable: false));
            DropColumn("dbo.Accounts", "AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "AccountNumber", c => c.Long(nullable: false));
            DropColumn("dbo.Accounts", "CardNumber");
        }
    }
}
