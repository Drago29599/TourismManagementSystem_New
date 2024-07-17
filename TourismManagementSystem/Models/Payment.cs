using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Display(Name ="Payment Date")]
        public DateTime PaymentDateTime { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Phone No.")]
        public long CustomerPhoneNumber { get; set; }


        public int PackageId { get; set; }

        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [Display(Name = "Payment Amount")]
        public int Amount { get; set; }

        [Display(Name = "No of Guests")]
        public int NoOfGuests { get; set; }

        [Display(Name = "Guest's Name")]
        public string GuestNames { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Pickup point")]
        public string PickupPoint { get; set; }

        [Display(Name = "Ticket Number")]
        public string TicketNumber { get; set; }





    }
}