using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class Package
    {
        public int PackId { get; set; } //scaler properties //primary key-identity  

        [Required(ErrorMessage ="Please enter pacakge name")]
        [Display(Name ="Pacakage Name")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Please enter pacakge price")]
        [Display(Name = " Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Please enter No of guests")]
        [Display(Name = "No of Guests")]
        public int NoOfPeoples { get; set; }

        [Required(ErrorMessage = "Please enter pacakge start date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter pacakge end date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please enter pickup point")]
        [Display(Name = "Pickup Point")]
        public string PickupPoint { get; set; }

        [MinLength(80, ErrorMessage = "The minimum length is 80 characters.")]
        [Required(ErrorMessage = "Please enter other pacakge details")]
        [Display(Name = "Pacakage Details")]
        public string PackageDetails { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter Destination")]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        public byte[] Image { get; set; }

        // Navigation property
        public ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Customer> FavoriteCustomer { get; set; }
    }
}