using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class Customer
    {
        
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter user name*")]
        [DisplayName("User Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmailId { get; set; }

        [Required(ErrorMessage = "Please enter password*")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit ")]
        [Display(Name = "Password")]
        [UIHint("Password")]
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }

        [Required(ErrorMessage = "Please enter contact number")]
        [DisplayName("Phone Number")]
        public long CustomerPhone { get; set; }

        [Required(ErrorMessage = "Please enter Residential Address")]
        [DisplayName("Residential Address")]
        public string CustomerAdress { get; set; }

        // Navigation property
        public ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Package> FavoritePackages { get; set; }
    }
}