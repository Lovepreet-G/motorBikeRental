using motorBikeRental.Models;
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

namespace motorBikeRental.Controllers
{
    public class bikeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //method to list bikes
        [HttpGet]
        [Route("api/bikeData/ListBikes")]
        public List<bikeDto> ListBikes()
        {
            List<bike> bikes = db.Bikes.ToList();
            List<bikeDto> bikeDtos = new List<bikeDto>();

            bikes.ForEach(b => bikeDtos.Add(new bikeDto()
            {
                BikeId = b.BikeId,
                BikeBrand = b.BikeBrand,
                BikeModel = b.BikeModel,
                BikeRate = b.BikeRate,
            }));

            return bikeDtos;
        }

        //method to find a bike
        [ResponseType(typeof(bike))]
        [HttpGet]
        [Route("api/bikeData/FindBike/{id}")]
        public IHttpActionResult FindBike(int id)
        {
            bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }
            bikeDto bikeDto = new bikeDto()
            {
                BikeId = bike.BikeId,
                BikeBrand = bike.BikeBrand,
                BikeModel = bike.BikeModel,
                BikeRate = bike.BikeRate,
            };
            

            return Ok(bikeDto);
        }

        //method to update a bike
        // POST: api/bikeData/UpdateBike/1
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/bikeData/UpdateBike/{id}")]
        public IHttpActionResult UpdateBike(int id, bike bike)
        {
            Debug.WriteLine("I have reached the update bike method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != bike.BikeId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + bike.BikeId);
                Debug.WriteLine("POST parameter" + bike.BikeBrand);
                Debug.WriteLine("POST parameter " + bike.BikeModel);
                Debug.WriteLine("POST parameter " + bike.BikeRate);
                return BadRequest();
            }

            db.Entry(bike).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bikeExists(id))
                {
                    Debug.WriteLine("bike not found");
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

        //Method to add bike
        // POST: api/bikeData/Addbike
        [ResponseType(typeof(bike))]
        [HttpPost]
        public IHttpActionResult AddBike(bike bike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bikes.Add(bike);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bike.BikeId }, bike);
        }

        //Method to delete a bike
        // POST: api/bikeData/DeleteBike/5
        [ResponseType(typeof(bike))]
        [HttpPost]
        public IHttpActionResult DeleteBike(int id)
        {
            bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }

            db.Bikes.Remove(bike);
            db.SaveChanges();

            return Ok();
        }
        private bool bikeExists(int id)
        {
            return db.Bikes.Count(e => e.BikeId == id) > 0;
        }
    }
}
