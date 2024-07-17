using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TourismManagementSystem.Models;

namespace TourismManagementSystem.Controllers
{
    public class BankController : Controller
    {
        //consuming the BankWebAPI here
      
        public ActionResult VerifyPayment(int id)
        {
            Account acc = new Account(); // Creating a new instance of the Account class
            return View(acc); // Return the View with the Account object as the model
        }

        [HttpPost]
        public ActionResult VerifyPayment(Account acc,int id)
        {
            
            HttpClient client = new HttpClient(); // Create a new instance of HttpClient
            //  base address for the HTTP client
            client.BaseAddress = new Uri("https://localhost:44357/api/");

            // To convert the Account object to JSON string
            string jsonData = JsonConvert.SerializeObject(acc);

            //  HTTP content with JSON data
            var content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json"); 


            // Send a POST request to the "BankAPI" endpoint with the JSON content
            Task<HttpResponseMessage> responseTask = client.PostAsync("BankAPI", content);
            responseTask.Wait(); // Waiting for the response


            // To Get the response from the task
            var response = responseTask.Result;

            // Check if the request was successful (status code 2xx)
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a boolean indicating payment verification result
                bool verificationResult = response.Content.ReadAsAsync<bool>().Result;
                if (verificationResult)
                {
                    // If the details are verified, redirect to the "BuyPackage" action in the "User" controller
                    return RedirectToAction("BuyPackage", "User", new { id = id });
                }
                else
                {
                    // Set an error message in the ViewBag
                    ViewBag.Message = "Payment verification failed.";
                    return View("PaymentError"); // Return the "PaymentError" view
                }
                
            }
            else
            {
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                return View("PaymentError");// Return the "PaymentError" view
            }

        }






    }
}