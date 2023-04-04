using FlightBookingSystem;
using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System;
using System.Linq;
using System.Web.Mvc;

public class BookingController : Controller
{
    private readonly AppDbContext _context;

    public BookingController()
    {
        _context = new AppDbContext();
    }

    // GET: /Booking/CreateBooking/{id}
    public ActionResult CreateBooking(int id)
    {
        var flight = _context.Flights.Find(id);

        if (flight == null)
        {
            return HttpNotFound();
        }

        int userId;
        if (Session["Id"] == null || !int.TryParse(Session["Id"].ToString(), out userId))
        {
            return RedirectToAction("Login", "User");
        }

        User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        Session["FlightId"] = flight.FlightId;
        Session["UserId"] = user.UserId;
        var booking = new Booking()
        {
            FlightId = flight.FlightId,
            UserId = user.UserId,
         
             Price = flight.Price
        };

        return View(booking);
    }

    // POST: /Booking/CreateBooking
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateBooking(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            return View(booking);
        }
        if (Session["FlightId"] == null || Session["UserId"] == null)
        {
            return RedirectToAction("Login", "User");
        }

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
        booking.FlightId = (int)Session["FlightId"];
        booking.UserId = (int)Session["UserId"];
        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { id = booking.Id });
    }

    // GET: /Booking/Confirmation/{id}
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
