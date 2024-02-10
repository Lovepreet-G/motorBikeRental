using motorBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace motorBikeRental.Controllers
{
    public class rentalController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static rentalController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/rentalData/");
        }
        // GET: rental/List
        // to get list of all rentals
        public ActionResult List()
        {
            string url = "ListRentals";
            HttpResponseMessage response = client.GetAsync(url).Result;            

            IEnumerable<rentalHistoryDto> rentals = response.Content.ReadAsAsync<IEnumerable<rentalHistoryDto>>().Result;
            
            return View(rentals);
        }

        // GET: rental/GetRentalsByBikeId/{id}
        // to get list of customers who rented a bike
        public ActionResult GetRentalsByBikeId(int id)
        {
            string url = "GetRentalsByBikeId/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<rentalHistoryDto> rentals = response.Content.ReadAsAsync<IEnumerable<rentalHistoryDto>>().Result;

            return View(rentals);
        }

        // GET: rental/GetRentalsByCustomerId/{id}
        // to get list of customers who rented a bike
        public ActionResult GetRentalsByCustomerId(int id)
        {
            string url = "GetRentalsByCustomerId/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<rentalHistoryDto> rentals = response.Content.ReadAsAsync<IEnumerable<rentalHistoryDto>>().Result;

            return View(rentals);
        }
    }
}