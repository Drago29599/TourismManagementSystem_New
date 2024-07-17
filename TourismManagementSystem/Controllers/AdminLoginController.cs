using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
    public class AdminLoginController : Controller
    {
        //Creating the object of Dbcontext class : TourismDbContext
        TourismDbContext db = new TourismDbContext();

        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        //creating post method for Login
        [HttpPost]
        public ActionResult Index(Admin adm)
        {
            //creating variable for user 
            var user = db.Admins.Where(model => model.AdminName == adm.AdminName && model.AdminPassword == adm.AdminPassword).FirstOrDefault();
            if (user != null)
            {
                // to record the login sessions
                Session.Add("AdminUserId", adm.AdminId);
                Session.Add("AdminUserName", adm.AdminName);
                
                TempData["LoginSuccessMessage"] = "<script>alert('Admin Login successfull') </script>"; // To show that login is successfull

                return RedirectToAction("Index", "AdminDashboard");  // after login it will redirect to user dashboard
                                         
            }

            else  // if login failed it will redirect to same login page with error msg
            {
                ViewBag.ErrorMessage = "Login Failed !! \n Please enter correct user name or password.";
                return View(); //"Index","AdminLogin"
            }
        }
    }
}