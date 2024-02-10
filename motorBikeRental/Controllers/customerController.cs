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
    }
}