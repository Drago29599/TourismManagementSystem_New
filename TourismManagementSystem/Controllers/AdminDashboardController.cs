using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
    public class AdminDashboardController : Controller
    {
        //Intialization object of DBcontext class
        TourismDbContext db = new TourismDbContext();

        // GET: AdminDashboard
        public ActionResult Index()
        {
            //This is used for more security if someonw trying go directly to AdminDashboard/index page
            if (Session["AdminUserId"] == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            else
            {
                return View();
            }
        }

        //Creating method for logout

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "AdminLogin");

        }

        //adding new Admin our Admin table
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            if (ModelState.IsValid == true) //if all data entered is valide
            {
                db.Admins.Add(admin);
                try { 
                    int a = db.SaveChanges(); //To save changes in database
                    if (a > 0)
                    {
                        TempData["SuccessMessage"] = "Admin added successfully!";
                        ModelState.Clear(); // to clear entered data from html page
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Opration failed, Please try again!";
                        
                    }
                }
                catch(DbEntityValidationException e)
                {
                    TempData["SuccessMessage"] = "Opration failed, Please try again!";
                }
                
                

            }
                return View();
        }

        //To get the list of all Admin details
        [HttpGet]
        public ActionResult GetAdmin()
        {
            var fetch = db.Admins.ToList();
            return View(fetch);
        }

        //To Edit the admin details
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var obj = db.Admins.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin adm)
        {
            
            try
            {
                db.Entry(adm).State = System.Data.Entity.EntityState.Modified; //used to modifi the admin
                db.SaveChanges();
                TempData["SuccessMessage"] = "Changes saved!";

            } 
            catch (DbEntityValidationException e)
            {
                TempData["SuccessMessage"] = "Operation Failed try again!";
            }
            
            return View();
        }

        
        //To delete admin
        [HttpGet]
        public ActionResult DeleteAdmin( int id)
        {
            var obj = db.Admins.Find(id); //To Get details of Admin as per ID
            return View(obj);
        }

        //To delete admin after confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminConfirmed(int id)
        {
            var adm = db.Admins.Find(id);

            if (adm == null)
            {
                return HttpNotFound();
            }

            db.Admins.Remove(adm); //to remove admin from admin table
            db.SaveChanges();
            TempData["SuccessMessage"] = "Admin deleted successfully!";
            return RedirectToAction("GetAdmin", TempData["SuccessMessage"]);
        }

        [HttpGet]
        public ActionResult GetBooking(string searchStr, int page = 1)
        {
            //This part is used for layout purpose
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }

            var fetch = db.payments.ToList();
            if (!string.IsNullOrEmpty(searchStr))
            {
                fetch = db.payments.Where(p => (p.CustomerName.Contains(searchStr) || p.PackageName.Contains(searchStr))).ToList();
            }
            if (fetch == null)
            {
                fetch = new List<Payment>(); // Create an empty list if fetch is null
            }
            //To order data in decending order by start date
            fetch = fetch.OrderByDescending(item => item.StartDate).ToList();

            int pageSize = 5; // Specify the page size
            int totalCount = fetch.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            ViewBag.pageNumber =page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchStr = searchStr;

            // Perform pagination
            var paginatedData = fetch.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Pass the paginated data to the view
            return View(paginatedData);
        }



        /*
        //To get all booking details 
        [HttpGet]
        public ActionResult GetBooking()
        {
            //This part is used for layout purpose
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var fetch = db.payments.ToList();
            if (fetch == null)
            {
                fetch = new List<Payment>(); // Create an empty list if fetch is null
            }

            return View(fetch);
        }

        */
        //for Canceling booking of customer
        public ActionResult CancelBooking(int id)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var obj = db.payments.Find(id);
            return View(obj);
        }

        //for booking Cancel confirmation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBookingConfirmed(int id)
        {
            
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var pay = db.payments.Find(id);
            if (pay == null)
            {
                
                return HttpNotFound();
            }

            db.payments.Remove(pay);
            int a = db.SaveChanges();
            if (a > 0)
            {

                TempData["SuccessMessage"] = $"Booking Canceled for Ticket No.: {pay.TicketNumber} !";

            }
            else
            {
                TempData["SuccessMessage"] = "Operation failed!";
            }
            return RedirectToAction("GetBooking", TempData["SuccessMessage"]);
        }
    }
}