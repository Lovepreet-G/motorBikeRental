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
    public class customerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static customerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/customerData/");
        }
        // GET: customer/ListForAdmin
        // List of customers with the privilage to update and delete
        public ActionResult ListForAdmin()
        {

            string url = "ListCustomers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<customerDto> customers = response.Content.ReadAsAsync<IEnumerable<customerDto>>().Result;

            return View(customers);
        }

        // GET: customer/Edit/{id}
        public ActionResult Edit(int id)
        {
            //grab the bike information
            //objective: communicate with our bike data api to retrieve one bike


            string url = "FindCustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            customerDto selectedcustomer = response.Content.ReadAsAsync<customerDto>().Result;

            return View(selectedcustomer);
        }

        // POST: customer/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, customer customer)
        {
            try
            {
                //serialize into JSON
                //Send the request to the API

                string url = "UpdateCustomer/" + id;


                string jsonpayload = jss.Serialize(customer);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/customerData/UpdateCustomer/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("ListForAdmin");
            }
            catch
            {
                return View();
            }
        }
    }
}