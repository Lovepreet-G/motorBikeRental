using motorBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace motorBikeRental.Controllers
{
    public class bikeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static bikeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/bikeData/");
        }
        // GET: bike/List
        // to list all the bike 
        public ActionResult List()
        {

            string url = "ListBikes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<bikeDto> bikes = response.Content.ReadAsAsync<IEnumerable<bikeDto>>().Result;
        
            return View(bikes);
           
        }

        // GET: bike/ListForAdmin
        // to show bike to the admin with links to update and delete the bike
        public ActionResult ListForAdmin()
        {

            string url = "ListBikes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<bikeDto> bikes = response.Content.ReadAsAsync<IEnumerable<bikeDto>>().Result;

            return View(bikes);

        }

        // GET: bike/New
        public ActionResult New()
        {
            return View();
        }

        // POST: bike/Create
        [HttpPost]
        public ActionResult Create(bike bike)
        {

            //objective: add a new Bike into our system using the API

            string url = "Addbike";

            string jsonpayload = jss.Serialize(bike);          

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListForAdmin");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }
    }
}