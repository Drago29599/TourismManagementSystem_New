using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class Admin
    {
        
        public int AdminId { get; set; }
        
        
        [Required(ErrorMessage = "Please enter a Designation")]
        [Display(Name = "Designation")]
        public string AdminDesignation { get; set; }

        [Required(ErrorMessage = "Please enter a Admin Name")]
        [Display(Name = "User Name")]
        [Index(IsUnique = true)]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit ")]
        [Display(Name = "Password")]
        [UIHint("Password")]
        public string AdminPassword { get; set; }

        // Navigation property
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}