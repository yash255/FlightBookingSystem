using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController()
        {
            _context = new AppDbContext();
        }

        // GET: Booking/Create
        public ActionResult CreateBooking(int? flightId, int userId)
        {
            // Retrieve the flight and user from the database
            var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            // If either the flight or user is not found, redirect to an error page
            if (flight == null || user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Create a new booking and set its default properties
            var booking = new Booking
            {
                Flight = flight,
                User = user,
                BookingTime = DateTime.Now
            };

            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            // Validate the booking model
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            // Calculate the price based on the flight's base price and the selected cabin class
            decimal price;
            switch (booking.CabinClass)
            {
                case CabinClass.Economy:
                    price = 1000;
                    break;
                case CabinClass.Business:
                    price = 2000;
                    break;
                case CabinClass.First:
                    price = 3000;
                    break;
                default:
                    return RedirectToAction("Error", "Home");
            }
            price *= booking.NoOfTickets;

            // Set the booking price and save changes to the database
            booking.Price = price;
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Confirmation");
        }


        public ActionResult Confirmation(int id)
        {
            var booking = _context.Bookings
                .Include("Flight")
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

    }
}
