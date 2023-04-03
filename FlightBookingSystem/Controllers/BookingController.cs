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
        public ActionResult CreateBooking(int id)
        {
            var flight = _context.Flights.Find(id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

           /* if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }*/

            var booking = new Booking()
            {
                FlightId = flight.Id,
                UserId = user.Id,
                BookingTime = DateTime.Now,
                Price = flight.Price
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Confirmation", new { id = booking.Id });
        }



        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(Booking booking)
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
