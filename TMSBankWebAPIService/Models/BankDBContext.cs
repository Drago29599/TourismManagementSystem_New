using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TMSBankWebAPIService.Models
{
    public class BankDBContext: DbContext
    {
        public BankDBContext():base("bankDbCon")
        { }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}