using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using motorBikeRental.Models;

namespace motorBikeRental.Controllers
{
    public class customerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //method to list customers
        [HttpGet]
        [Route("api/customerData/ListCustomers")]
        public List<customerDto> ListCustomers()
        {
            List<customer> customers = db.customers.ToList();
            List<customerDto> customerDtos = new List<customerDto>();

            customers.ForEach(c => customerDtos.Add(new customerDto()
            {
                customerId = c.customerId,                
                customerName = c.customerName,
                customerAddress = c.customerAddress,
                customerPhone = c.customerPhone
            }));

            return customerDtos;
        }

        //method to find a customer
        [ResponseType(typeof(customer))]
        [HttpGet]
        [Route("api/customerData/FindCustomer/{id}")]
        public IHttpActionResult FindCustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customerDto customerDto = new customerDto()
            {
                customerId = customer.customerId,
                customerName = customer.customerName,
                customerAddress = customer.customerAddress,
                customerPhone = customer.customerPhone
            };


            return Ok(customerDto);
        }

        //method to update a customer
        // POST: api/customerData/UpdateCustomer/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/customerData/UpdateCustomer/{id}")]
        public IHttpActionResult UpdateCustomer(int id, customer customer)
        {
            Debug.WriteLine("I have reached the update customer method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != customer.customerId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + customer.customerId);
                Debug.WriteLine("POST parameter" + customer.customerName);
                Debug.WriteLine("POST parameter " + customer.customerAddress);
                Debug.WriteLine("POST parameter " + customer.customerPhone);
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
                {
                    Debug.WriteLine("customer not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        //Method to add customer
        // POST: api/customerData/AddCustomer
        [ResponseType(typeof(bike))]
        [HttpPost]
        public IHttpActionResult AddCustomer(customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.customerId }, customer);
        }

        //Method to delete a customer
        // POST: api/customerData/DeleteCustomer/1
        [ResponseType(typeof(customer))]
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.customers.Remove(customer);
            db.SaveChanges();

            return Ok();
        }
        private bool customerExists(int id)
        {
            return db.customers.Count(c => c.customerId == id) > 0;
        }

    }
}
