using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class TourismRepository
    {
        private readonly TourismDbContext db = new TourismDbContext();


        //Method for uploading Imagin in database
        public int UploadImageInDataBase(HttpPostedFileBase file, Package package)
        {
            //converting Image data to bytes
            package.Image = ConvertToBytes(file);


            // Create a new Package object with the required properties
            var content = new Package
            {

               PackageName = package.PackageName,
               Price = package.Price,
               NoOfPeoples = package.NoOfPeoples,
               StartDate = package.StartDate,
               EndDate = package.EndDate,
               PickupPoint = package.PickupPoint,
               PackageDetails = package.PackageDetails,
               Country = package.Country,
               State = package.State,
               Destination = package.Destination,
               Image = package.Image

            };

            // Adding the new Package object to the Packages DbSet
            db.Packages.Add(content);
            try {
                int i = db.SaveChanges();

                if (i == 1)
                {
                    // Successful save operation
                    return 1;

                }
                else
                {
                    // Save operation failed
                    return 0;

                }
            }catch(Exception ex)
            {
                // Save operation failed
                return 0;
            }
            

        }

        // Method for converting Image data into bytes array
        public byte[] ConvertToBytes(HttpPostedFileBase image)

        {

            byte[] imageBytes = null;

            // Create a BinaryReader to read the image data from the InputStream of the HttpPostedFileBase object
            BinaryReader reader = new BinaryReader(image.InputStream);

            // Read the bytes of the image into the imageBytes array
            imageBytes = reader.ReadBytes((int)image.ContentLength);

            return imageBytes;

        }


        //for editing package
        public int UploadImageInDataBase2(HttpPostedFileBase file, Package package)

        {

            if (file.ContentLength>0) { package.Image = ConvertToBytes(file); }
            

            var pacakgeInDb = db.Packages.Single(P => P.PackId == package.PackId);
                pacakgeInDb.PackId = package.PackId;
                pacakgeInDb.PackageName = package.PackageName;
                pacakgeInDb.NoOfPeoples = package.NoOfPeoples;
                pacakgeInDb.Price = package.Price;
                pacakgeInDb.PickupPoint = package.PickupPoint;
                pacakgeInDb.PackageDetails = package.PackageDetails;
                pacakgeInDb.StartDate = package.StartDate;
                pacakgeInDb.EndDate = package.EndDate;
                pacakgeInDb.Country = package.Country;
                pacakgeInDb.State = package.State;
                pacakgeInDb.Destination = package.Destination;
            if (file.ContentLength > 0) {
                pacakgeInDb.Image = package.Image;
            }

            try {
                int i = db.SaveChanges();

                if (i == 1)

                {

                    return 1;

                }

                else

                {

                    return 0;

                }
            } catch (Exception ex)
            {
                return 0;
            }
            

        }
    }
}