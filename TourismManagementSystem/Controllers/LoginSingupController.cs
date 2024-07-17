using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
    public class LoginSingupController : Controller
    {
        //Creating the object of Dbcontext class : TourismDbContext
        TourismDbContext db = new TourismDbContext();

        // GET: LoginSingup
        public ActionResult Index()
        {
            Customer cust = new Customer();
            return View(cust);
        }

        //creating post method for Login
        [HttpPost]
        public ActionResult Index(Customer customer)
        {
            //creating variable for user 
            var user = db.Customers.Where(model => model.CustomerName == customer.CustomerName && model.CustomerPassword == customer.CustomerPassword).FirstOrDefault();
            if(user != null)
            {
                // Session["UserId"] = customer.CustomerId.ToString(); // to record the login sessions
                Session.Add("UserId", user.CustomerId);
                Session.Add("UserName", user.CustomerName);
             
                TempData["LoginSuccessMessage"] = "<script>alert('Login successfull') </script>"; // To show that login is successfull
                
                return RedirectToAction("Index", "User");  // after login it will redirect to user dashboard
            
            }

            else  // if login failed it will redirect to same login page with error msg
            {
                ViewBag.ErrorMessage = "Login Failed !! \n Please enter correct user name or password";
                return View();
            }
            
        }
        public ActionResult SingUp()
        {
            return View();
        }

        //creating post method for signup
        [HttpPost]
        public ActionResult SingUp(Customer customer)
        {
            if(ModelState.IsValid==true) //if all data entered is valide
            {
                db.Customers.Add(customer);
                int a = db.SaveChanges(); // If changes saved then will return >0 else 0
                if(a >0)
                {
                    ViewBag.InsertMessage = "<script>alert('Registration successfully') </script>";
                    ModelState.Clear(); // to clear entered data from html page
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Registration Failed') </script>";
                }

            }
            return View();
        }
    }
}