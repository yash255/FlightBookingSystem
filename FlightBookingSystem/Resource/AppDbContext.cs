using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FlightBookingSystem.Models;
using System.Reflection.Emit;
using System.Data.Entity.Core.Metadata.Edm;


namespace FlightBookingSystem.Resource
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
    
        public DbSet<Booking> Bookings { get; set; }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           





        }

    }
}