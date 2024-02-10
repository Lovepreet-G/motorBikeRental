using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorBikeRental.Models
{
    public class rentalHistory
    {
        [Key]
        public int rentalId { get; set; }
        [ForeignKey("bike")]
        public int BikeId { get; set; }
        public virtual bike bike { get; set; }

        [ForeignKey("customer")]
        public int customerId { get; set; }
        public virtual customer customer { get; set; }

        public DateTime from { get; set; }

        public DateTime to { get; set; }

    }
    public class rentalHistoryDto
    {
        public int rentalId { get; set;}
        public string customerName { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public int BikeId { get; set; }
        public string BikeBrand { get; set; }
        public string BikeModel { get; set; }
        public int BikeRate { get; set; }
        public int customerId { get; set; }
        


    }
}