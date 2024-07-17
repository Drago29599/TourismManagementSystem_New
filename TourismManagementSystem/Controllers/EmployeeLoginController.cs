using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
    public class EmployeeLoginController : Controller
    {
        //Creating the object of Dbcontext class : TourismDbContext
        TourismDbContext db = new TourismDbContext();

        // GET: EmployeeLogin
        public ActionResult Index()
        {
            return View();
        }



        //creating post method for Login
        [HttpPost]
        public ActionResult Index(Employee emp)
        {
            //creating variable for user 
            var user = db.Employees.Where(model => model.EmpUserName == emp.EmpUserName && model.EmployeePass == emp.EmployeePass).FirstOrDefault();
            if (user != null)
            {
                // to record the login sessions
                Session.Add("EmpUserId", user.EmployeeId);
                Session.Add("EmpUserName", user.EmpUserName);
                
                
                TempData["LoginSuccessMessage"] = "<script>alert('Employee Login successfull') </script>"; // To show that login is successfull

                return RedirectToAction("Index", "Employee");  // after login it will redirect to user dashboard

            }
            else  // if login failed it will redirect to same login page with error msg
            {
                ViewBag.ErrorMessage = "Login Failed !! \n Please enter correct user name or password.";
                return View(); //"Index","EmployeeLogin"
            }
        }
    }
}