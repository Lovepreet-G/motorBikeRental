using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace motorBikeRental.Models
{
    public class customer
    {
        [Key]
        public int customerId { get; set; }

        public string customerName { get; set; }

        public string customerAddress { get; set; }

        public int customerPhone { get; set; }
        
    }

    public class customerDto
    {
        public int customerId { get; set; }

        public string customerName { get; set; }

        public string customerAddress { get; set; }

        public int customerPhone { get; set; }

    }
}