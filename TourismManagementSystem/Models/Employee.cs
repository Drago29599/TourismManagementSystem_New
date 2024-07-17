using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TourismManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter employee user name")]
        [Display(Name = "Employee User Name")]
        public string EmpUserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit ")]
        [Display(Name = "Employee Password")]
        [UIHint("Password")]
        public string EmployeePass { get; set; }

        [Required(ErrorMessage = "Please enter employee designation")]
        [Display(Name = "Designation")]
        public string EmployeeRole { get; set; }

        [Required(ErrorMessage = "Please enter employee first name")]
        [Display(Name = "First Name")]
        public string EmpFirstName { get; set; }

        [Required(ErrorMessage = "Please enter employee last name")]
        [Display(Name = "Last Name")]
        public string EmpLastName { get; set; }

        [Required(ErrorMessage = "Please enter employee email address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmpEmail { get; set; }

        [Required(ErrorMessage = "Please enter employee contact no.")]
        [Display(Name = "Contact Number")]
        public long EmpPhoneNumber { get; set;}

        [Required(ErrorMessage = "Please enter employee residential address")]
        [Display(Name = "Residential Address")]
        public string EmpResidentialAddress { get; set; }

        public ICollection<Payment> Payments { get; set; }

    }
}