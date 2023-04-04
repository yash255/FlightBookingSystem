using FlightBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Controllers
{
   

    public class AdminFlightController : Controller
    {
        private readonly Resource.AppDbContext _context;

        public AdminFlightController()
        {
            _context = new Resource.AppDbContext();
        }



        // CRUD operations for flights

        public ActionResult FlightsList()
        {
            var flights = _context.Flights.ToList();
            return View(flights);
        }

        public ActionResult CreateFlight()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFlight(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return View(flight);
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();

            return RedirectToAction("FlightsList");
        }

        public ActionResult EditFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            return View(flight);
        }

        [HttpPost]
        public ActionResult EditFlight(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return View(flight);
            }

            var existingFlight = _context.Flights.FirstOrDefault(f => f.FlightId == flight.FlightId);

            if (existingFlight == null)
            {
                return HttpNotFound();
            }

            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.ArrivalCity = flight.ArrivalCity;
            existingFlight.DepartureCity = flight.DepartureCity;
            existingFlight.DepartureTime = flight.DepartureTime;
            existingFlight.ArrivalTime = flight.ArrivalTime;
            existingFlight.Price = flight.Price;

            _context.SaveChanges();

            return RedirectToAction("FlightsList");
        }

        public ActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            _context.Flights.Remove(flight);
            _context.SaveChanges();

            return RedirectToAction("FlightsList");
        }

    }
}