using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motorBikeRental.Models
{
    public class bike
    {
        //what describe a bike ?
        [Key]
        public int BikeId { get; set; }

        public string BikeBrand { get; set; }

        public string BikeModel {  get; set; }
        //Rate per day in Cad
        public int BikeRate { get; set; }

        
    }

    public class bikeDto
    {
        
        public int BikeId { get; set;}
        public string BikeBrand { get; set;}
        public string BikeModel { get; set;}
        public int BikeRate { get; set;}

    }
}