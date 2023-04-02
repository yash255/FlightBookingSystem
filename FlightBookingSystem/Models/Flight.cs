using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightBookingSystem.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FlightNumber { get; set; }

        

        
        public string DepartureCity { get; set; }

       
        public string ArrivalCity { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }

}