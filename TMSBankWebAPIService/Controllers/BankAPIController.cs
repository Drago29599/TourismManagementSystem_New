using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TMSBankWebAPIService.Models;

namespace TMSBankWebAPIService.Controllers
{
    public class BankAPIController : ApiController
    {
        BankDBContext cts = new BankDBContext();

        //https://localhost:44357/api/BankAPI
        [HttpPost]
        public bool Post([FromBody]Account account)
        { 
            // Perform validation on the entered details
            if (string.IsNullOrEmpty(account.AccountHolderName) || account.CardNumber == 0
                || string.IsNullOrEmpty(account.CardType) || account.CCVNumber == 0
                || account.CardPin == 0)
            {
                // If any required field is missing or empty, return bad request
                return false;
            }

            // Verify the card details against your database
            

            var matchingCard = cts.Accounts.FirstOrDefault(c =>
                c.CardType == account.CardType &&
                c.CardNumber == account.CardNumber &&
                c.AccountHolderName == account.AccountHolderName &&
                c.CCVNumber == account.CCVNumber &&
                c.CardPin == account.CardPin);

            if (matchingCard != null)
            {
                // If the details are verified

                // Return a true response if the details are verified

                return true;
            }

            // If no matching card details found in the database, redirect to the "Buypackage" action in the "User" controller
            return false;
            
    }
    }
}
