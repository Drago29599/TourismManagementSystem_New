namespace TMSBankWebAPIService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDCardType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "CardType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "CardType");
        }
    }
}
