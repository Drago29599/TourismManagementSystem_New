using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class Account
    {


        [Key]
        public int AccountId { get; set; }

        public long CardNumber { get; set; }

        public string AccountHolderName { get; set; }

        public string CardType { get; set; }

        public int CCVNumber { get; set; }

        public int CardPin { get; set; }
    }
}