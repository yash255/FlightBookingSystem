﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string PassengerName { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
       
        public string Email { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public byte NoOfTickets { get; set; }

        [Required]
        public CabinClass CabinClass { get; set; }

     

        [Required]
        public DateTime BookingTime { get; set; }

        [Required]
        public decimal Price { get; set; }



        
       public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


        [Required]
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
    }

}