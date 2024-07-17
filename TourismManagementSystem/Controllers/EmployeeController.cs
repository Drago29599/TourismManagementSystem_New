using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
   
    public class EmployeeController : Controller
    {
        TourismDbContext db = new TourismDbContext();
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["EmpUserId"] == null)
            {
                return RedirectToAction("Index","EmployeeLogin");
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
            return RedirectToAction("Index", "EmployeeLogin");

        }

        //To get the list of all employees
        [HttpGet]
        public ActionResult GetEmployee()
        {
            var fetch = db.Employees.ToList();
            return View(fetch);
        }

        //To get employee by Id: Employee profile 
        [HttpGet]
        public ActionResult GetEmployeeById()
        {
            try
            {
                var empId = (int)Session["EmpUserId"];

                // Retrieve employee based on the employee ID by using session variable
                var myProfile = db.Employees.Where(e =>e.EmployeeId == empId).ToList();
                

                // Pass the employee to the view
                return View(myProfile);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex;
                return View("Index", "EmployeeLogin");
            }

        }

        // Methods to Add Employee to Employee table
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            if (ModelState.IsValid == true) //if all data entered is valid
            {
                try {
                    db.Employees.Add(emp);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["SuccessMessage"] = "Employee added successfully!";
                        ModelState.Clear(); // to clear entered data from html page
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Opration failed, Please try again!";
                    }
                }
                catch (DbEntityValidationException e)
                {
                    TempData["SuccessMessage"] = "Opration failed, Please try again!";
                }


            }
            return View();
        }

        

        //To Edit the Employee details
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var obj = db.Employees.Find(id);
            return View(obj);
        }


        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            try
            {
                db.Entry(emp).State = System.Data.Entity.EntityState.Modified; //used to modifi the admin
                TempData["SuccessMessage"] = "Changes saved!";
                db.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                TempData["SuccessMessage"] = "Opration failed try again!";
            }
            
            return View();
        }


        //To Get details of Admin as per ID
        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            var obj = db.Employees.Find(id);
            return View(obj);
        }

        //To delete admin after confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployeeConfirmed(int id)
        {
            var emp = db.Employees.Find(id);

            if (emp == null)
            {
                return HttpNotFound();
            }

            db.Employees.Remove(emp);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Employee deleted successfully!";
            return RedirectToAction("GetEmployee", TempData["SuccessMessage"]);
        }

        
        //buying package for customer 
        public ActionResult BuyPackage()
        {
            var packages = db.Packages.ToList();
            ViewBag.Packages = packages; //passing package object using viewbag
            return View();
        }

        [HttpPost]
        public ActionResult BuyPackage(FormCollection frm)
        {
            //using FormCollection to collect user entered data

            try
            {
                int id = Int32.Parse(frm["pkgid"]);           //For package ID
                int custid = Int32.Parse(frm["custid"]);      //For customer ID
                string guestNames = frm["guest"];             //for Entered guest's names
                var package = db.Packages.Find(id);           //Finding package by Id
                string ticketNumber = GenerateTicketNumber();  //Generating ticket number
                var customer = db.Customers.Find(custid);      //Finding customer by Id
                if (package != null & customer != null)        //If both are not null  
                {
                    //Assiging all deatils to payment object
                    var payment = new Payment()
                    {
                        PaymentDateTime = DateTime.Now,
                        PackageId = id,
                        CustomerId = custid,
                        CustomerName = customer.CustomerName,
                        Amount = package.Price,
                        PackageName = package.PackageName,
                        CustomerPhoneNumber = customer.CustomerPhone,
                        StartDate = package.StartDate,
                        EndDate = package.EndDate,
                        PickupPoint = package.PickupPoint,
                        NoOfGuests = package.NoOfPeoples,
                        TicketNumber = ticketNumber,
                        GuestNames = guestNames
                    };
                    //Adding payment object to payments table
                    db.payments.Add(payment);
                    db.SaveChanges();

                    // Redirect to puchase  confirmation page
                    return View("PurchaseConfirmation", payment);
                }
                else {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
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

        //Edit customer details 
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            if (Session["EmpUserId"] != null)
            {
                ViewBag.Role = "Employee";
            }
            else
            {
                ViewBag.Role = "Admin";
            }

            var cust = db.Customers.Find(id); ;
            return View(cust);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer cust)
        {
            if (Session["EmpUserId"] != null)
            {
                ViewBag.Role = "Employee";
            }
            else
            {
                ViewBag.Role = "Admin";
            }

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