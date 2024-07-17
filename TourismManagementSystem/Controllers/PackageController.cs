using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;



namespace TourismManagementSystem.Controllers
{
    public class PackageController : Controller
    {
        TourismDbContext db = new TourismDbContext();

        // GET: Pacakge
        public ActionResult Index()
        {
            return View();
        }

        //To get the list of all Packages
        [HttpGet]
        public ActionResult GetPackage(string searchStr, int page=1)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }


            var fetch = db.Packages.ToList();
            if (!string.IsNullOrEmpty(searchStr))
            {
                fetch = db.Packages.Where(p => (p.Destination.Contains(searchStr) || p.Country.Contains(searchStr) || p.State.Contains(searchStr))).ToList();
            }

            //To fetech 9 records per page
            int recordsPerPage = 6;
            int totalRecords = fetch.Count();

            //To get the total no of pages
            int totalPages = totalRecords > recordsPerPage ? (int)Math.Ceiling(totalRecords / (double)recordsPerPage) : 1;

            ViewBag.TotalPages = totalPages;
            ViewBag.pageNumber = page;
            ViewBag.SearchStr = searchStr;

            //To get desired records per page
            fetch = fetch.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToList();

            if (fetch == null)
            {
                fetch = new List<Package>(); // Create an empty list if fetch is null
            }
            
            return View(fetch);
        }
        
        //To Edit the Pacakge details
        [HttpGet]
        public ActionResult EditPackage(int id)
        {

       
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var obj = db.Packages.SingleOrDefault(p => p.PackId == id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditPackage(Package pack)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }

            //Getting data from id = ImageData from view and storing it to file 
            HttpPostedFileBase file = Request.Files["ImageData"];

            //Creating object for repository : TourismRepository
            TourismRepository service = new TourismRepository();

            //calling UploadImageInDataBase2 from repository
            int i = service.UploadImageInDataBase2(file, pack);

            if (i == 1)
            {

                TempData["SuccessMessage"] = "Changes saved successfully!";
                ModelState.Clear(); // to clear entered data from html page
                return View();

            }
            else 
            {
                TempData["SuccessMessage"] = "Opration failed!";
                return View(pack);
            }
            

        }

        //adding new Pacakge to Pacakge table
        [HttpGet]
        public ActionResult AddPackage()
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPackage(Package pack)
        {
            //Getting data from id = ImageData from view and storing it to file
            HttpPostedFileBase file = Request.Files["ImageData"];

            //Creating object for repository : TourismRepository
            TourismRepository service = new TourismRepository();

            //calling UploadImageInDataBase from repository
            int i = service.UploadImageInDataBase(file, pack);

            if (i == 1)
            {

                TempData["SuccessMessage"] = "Package added successfully!";
                ModelState.Clear(); // to clear entered data from html page
                return RedirectToAction("AddPackage","Package");

            }
            TempData["SuccessMessage"] = "Opration failed!";
            return View(pack);

        }


        //for retriving image from database
        public ActionResult RetrieveImage(int id)
        {
            //Geting image data in bytes array from database
            byte[] cover = GetImageFromDataBase(id);

            if (cover != null)
            {
                //if cover is not null then return file cover as Image type
                return File(cover, "image/jpg");

            }
            else
            {
                //else return null
               return null;
            }
        }

        //method to retriving data in the form of bytes from database
        public byte[] GetImageFromDataBase(int Id)

        {
            //for getting Image bytes from database 
            var q = from temp in db.Packages where temp.PackId == Id select temp.Image;

            //Adding image bytes to cover array
            byte[] cover = q.First();

            return cover;

        }

        //To Get details of Package as per ID
        [HttpGet]
        public ActionResult DeletePackage(int id)
        {
            if (Session["AdminUserId"] != null)
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                ViewBag.Role = "Employee";
            }
            var obj = db.Packages.Find(id);
            return View(obj);
        }

        //To delete Package after confirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePackageConfirmed(int id)
        {
            var pack = db.Packages.Find(id);

            if (pack == null)
            {
                return HttpNotFound();
            }

            db.Packages.Remove(pack);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Package deleted successfully!";

            return RedirectToAction("GetPackage", TempData["SuccessMessage"]);
        }
    }
}