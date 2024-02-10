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
    public class rentalDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //method to list rentals
        [HttpGet]
        [Route("api/rentalData/ListRentals")]
        public List<rentalHistoryDto> ListRentals()
        {
            List<rentalHistory> rentals = db.rentalHistories.ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                rentalId = r.rentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.customer.customerId,
                customerName = r.customer.customerName,
                
            }));

            return rentalHistoryDtos;
        }

        //method to get list of customers who rented a bike {id}
        [HttpGet]
        [Route("api/rentalData/GetRentalsByBikeId/{id}")]
        public List<rentalHistoryDto> GetRentalsByBikeId(int id)
        {
            List<rentalHistory> rentals = db.rentalHistories.Where(r => r.BikeId == id).ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                rentalId = r.rentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.customer.customerId,
                customerName = r.customer.customerName,

            }));

            return rentalHistoryDtos;
        }

        //method to get list of bike rented by a customer
        [HttpGet]
        [Route("api/rentalData/GetRentalsByCustomerId/{id}")]
        public List<rentalHistoryDto> GetRentalsByCustomerId(int id)
        {
            List<rentalHistory> rentals = db.rentalHistories.Where(r => r.customerId == id).ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                rentalId = r.rentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.customer.customerId,
                customerName = r.customer.customerName,

            }));

            return rentalHistoryDtos;
        }
    }
}
