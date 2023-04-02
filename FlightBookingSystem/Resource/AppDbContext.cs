using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FlightBookingSystem.Models;
using System.Reflection.Emit;

namespace FlightBookingSystem.Resource
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
     //   public DbSet<City> Cities { get; set; }
        public DbSet<Booking> Bookings { get; set; }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

           /* modelBuilder.Entity<Flight>()
                .HasRequired(f => f.DepartureCity)
                .WithMany(c => c.DepartureFlights)
                .HasForeignKey(f => f.DepartureCityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .HasRequired(f => f.ArrivalCity)
                .WithMany(c => c.ArrivalFlights)
                .HasForeignKey(f => f.ArrivalCityId)
                .WillCascadeOnDelete(false);*/


           
        }

    }
}