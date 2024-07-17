using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace TourismManagementSystem.Controllers
{
    public class UserController : Controller
    {
        TourismDbContext db = new TourismDbContext();
        // GET: User

       
        public ActionResult Index(int pg = 1)
        {
           

            //This is used for more security if some trying go directly to user/index page
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index","LoginSingup");
            }
            else
            {
                try {

                    var Date = DateTime.Now.Date.AddDays(2); ;

                    //For getting list of active packages in my index view
                    var pack = db.Packages.Where(p => p.StartDate > Date).ToList();
                    //To fetech 9 records per page
                    int recordsPerPage = 9;
                    int totalRecords = pack.Count();
                    int totalPages = totalRecords > recordsPerPage ? (int)Math.Ceiling(totalRecords / (double)recordsPerPage) : 1;

                    ViewBag.TotalPages = totalPages;
                    ViewBag.pageNumber = pg;
                    pack = pack.Skip((pg - 1) * recordsPerPage).Take(recordsPerPage).ToList();
                    return View(pack);

                } catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Index", "LoginSingup");
                }
                
            }
            
        }
        [HttpPost]
        public ActionResult Index(string searchStr, int pg=1)
        {

            var pack = db.Packages.ToList();
            if (!string.IsNullOrEmpty(searchStr))
            {
                pack = db.Packages.Where(p=>(p.Destination.Contains(searchStr) || p.Country.Contains(searchStr) || p.State.Contains(searchStr))).ToList();
            }
            
            //To fetech 9 records per page
            int recordsPerPage = 9;
            int totalRecords = pack.Count();
            int totalPages = totalRecords > recordsPerPage ? (int)Math.Ceiling(totalRecords / (double)recordsPerPage) : 1;

            ViewBag.TotalPages = totalPages;
            ViewBag.pageNumber = pg;
            ViewBag.SearchStr = searchStr;

            pack = pack.Skip((pg - 1) * recordsPerPage).Take(recordsPerPage).ToList();
            if(pack.Count() == 0)
            {
                TempData["ErrorMsg"] = "Enter Package not found !!";
                return RedirectToAction("Index");
            }
            return View(pack);
                
        
        }

        //Creating method for logout

        public ActionResult Logout() 
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginSingup");

        }

        //For getting all customer details
        [HttpGet]
       public ActionResult GetCustomer(string searchStr, int page = 1)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var fetch = db.Customers.ToList();
            if (!string.IsNullOrEmpty(searchStr))
            {
                fetch = db.Customers.Where(p => (p.CustomerName.Contains(searchStr))).ToList();
            }
            //To fetech 8 records per page
            int recordsPerPage = 8;
            int totalRecords = fetch.Count();
            int totalPages = totalRecords > recordsPerPage ? (int)Math.Ceiling(totalRecords / (double)recordsPerPage) : 1;

            ViewBag.TotalPages = totalPages;
            ViewBag.pageNumber = page;
            ViewBag.SearchStr = searchStr;
            fetch = fetch.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToList();
            return View(fetch);
        }
       

        //Adding new Customer to our Customers table
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer cust)
        {
            if (ModelState.IsValid == true) //if all data entered is valide
            {
                db.Customers.Add(cust);
                try
                {
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["SuccessMessage"] = "Customer added successfully!";
                        ModelState.Clear(); // to clear entered data from html page
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Operation failed!";
                    }
                }
                catch (DbEntityValidationException e)
                {
                    TempData["SuccessMessage"] = "Operation failed!";
                }



            }
            return View();
        }

        //For contact to customer support executive
        public ActionResult Contact()
        {
            return View();
        }

        
        

        //For purchase of pacakge
        [HttpGet]
        public ActionResult BuyPackage(int id)
        {
            var package = db.Packages.SingleOrDefault(p => p.PackId == id); // selecting package by Id
            var customerId = (int)Session["UserId"];   //Getting user id by using sessions
            string ticketNumber = GenerateTicketNumber();  // Method to generate tickets
            var customer = db.Customers.Find(customerId);  //To find customer data from DB by using ID

            

            var payment = new Payment()
            {
                PaymentDateTime = DateTime.Now,
                PackageId = id,
                CustomerId = customerId,
                CustomerName = customer.CustomerName,
                Amount = package.Price,
                PackageName = package.PackageName,
                CustomerPhoneNumber = customer.CustomerPhone,
                StartDate = package.StartDate,
                EndDate = package.EndDate,
                PickupPoint = package.PickupPoint,
                NoOfGuests = package.NoOfPeoples,
                TicketNumber = ticketNumber,
                GuestNames = ""
            };
            
            //return View(package);
            return View("AddGuest",payment);
        }

        [HttpPost]
        public ActionResult BuyPackage(Payment payment)
        {

            //checking with account details 
            
                db.payments.Add(payment);
                db.SaveChanges();

            // Redirect to the PurchaseConfirmation page 
            return View("PurchaseConfirmation", payment);
            
        }


        //To get customer Booking by Id
        [HttpGet]
        public ActionResult CustomerBookings()
        {
            try
            {
                var customerId = (int)Session["UserId"];  //Getting customer by session variable Id

                // Retrieve the bookings based on the customer ID
                var bookings = db.payments.Where(p => p.CustomerId == customerId).ToList();

                // Pass the bookings to the view
                return View(bookings);

            } catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex;
                return View("Index","LoginSingup");
            }
            
        }


        //To generate ticket number
        private string GenerateTicketNumber()
        {
            // Generate a random ticket number with the format "OTXXXX"
            Random random = new Random();
            string ticketNumber = "OT" + random.Next(10000, 99999).ToString();

            return ticketNumber;
        }


        //Method for Cancel booking by customer
        [HttpGet]
        public ActionResult CancelBooking(int id)
        {
            var obj = db.payments.Find(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBookingConfirmed(int id)
        {
            var pay = db.payments.Find(id);

            if (pay == null)
            {
                return HttpNotFound();
            }

            db.payments.Remove(pay);
            TempData["SuccessMessage"] = $"Booking canceled for Ticket No.: {pay.TicketNumber} !";
            db.SaveChanges();

            return RedirectToAction("CustomerBookings", TempData["SuccessMessage"]);
        }

        //For getting user by Id: User Profile
        [HttpGet]
        public ActionResult GetUserById()
        {
            try
            {
                var userId = (int)Session["UserId"];

                // Retrieve Customer based on the customer ID
                var myProfile = db.Customers.Where(e => e.CustomerId == userId).ToList();


                // Pass the my profile to the view
                return View(myProfile);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex;
                return View("Index", "EmployeeLogin");
            }

        }


        //Edit customer details 
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            

            var cust = db.Customers.Find(id); ;
            return View(cust);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer cust)
        {
            

            if (ModelState.IsValid == true)
            {

                var customerInDb = db.Customers.Single(C => C.CustomerId == cust.CustomerId);
                customerInDb.CustomerName = cust.CustomerName;
                customerInDb.CustomerPhone = cust.CustomerPhone;
                customerInDb.CustomerAdress = cust.CustomerAdress;
                customerInDb.CustomerEmailId = cust.CustomerEmailId;
                customerInDb.CustomerPassword = cust.CustomerPassword;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Changes saved!";
                return View();

            }
            else
            {
                return HttpNotFound();
            }

        }
    }
}