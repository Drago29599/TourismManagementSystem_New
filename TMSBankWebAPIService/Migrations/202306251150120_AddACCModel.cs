namespace TMSBankWebAPIService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddACCModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Long(nullable: false),
                        AccountHolderName = c.String(),
                        CCVNumber = c.Int(nullable: false),
                        CardPin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
