using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System.Web.Mvc;
using System;
using System.Linq;
using FlightBookingSystem;

public class BookingController : Controller
{
    private readonly AppDbContext _context;

    public BookingController()
    {
        _context = new AppDbContext();
    }

    [Route("booking/create/{id:int}")]
    public ActionResult CreateBooking(int id)
    {
        var flight = _context.Flights.Find(id);

        if (flight == null)
        {
            return HttpNotFound();
        }

        int userId = Convert.ToInt32(Session["Id"]);

        User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        var booking = new Booking()
        {
            FlightId = flight.FlightId,
            UserId = user.UserId,
            BookingTime = DateTime.Now,
            Price = flight.Price,

        };

        return View(booking);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("booking/create")]
    public ActionResult CreateBooking(Booking booking)
    {
        // Validate the booking model
        if (!ModelState.IsValid)
        {
            return View(booking);
        }

        int userId = Convert.ToInt32(Session["Id"]);

        // Check if the user ID exists in the database
        User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
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
        booking.BookingTime = DateTime.Now;
        booking.UserId = userId;

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { id = booking.Id });
    }

}
