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

        /// <summary>
        /// Grabs the authentication cookie sent to this controller.
        /// For proper WebAPI authentication, you can send a post request with login credentials to the WebAPI and log the access token from the response. The controller already knows this token, so we're just passing it up the chain.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
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

        // GET: bike/Edit/{id}
        public ActionResult Edit(int id)
        {
            //grab the bike information
            //objective: communicate with our bike data api to retrieve one bike
            

            string url = "FindBike/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            bikeDto selectedbike = response.Content.ReadAsAsync<bikeDto>().Result;
           
            return View(selectedbike);
        }

        // POST: bike/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, bike bike)
        {
            try
            {
                //serialize into JSON
                //Send the request to the API

                string url = "UpdateBike/" + id;


                string jsonpayload = jss.Serialize(bike);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/bikeData/UpdateBike/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                //Debug.WriteLine(response.IsSuccessStatusCode);
                return RedirectToAction("ListForAdmin");
            }
            catch
            {
                return View();
            }

        }
        // GET: bike/DeleteConfirm/1
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindBike/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            bikeDto selectedbike = response.Content.ReadAsAsync<bikeDto>().Result;

            return View(selectedbike);
        }

        // POST: bike/Delete/1
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();//get token credentials
            string url = "bikeData/DeleteBike/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}