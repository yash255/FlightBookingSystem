using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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



        public ActionResult FlightsList()
        {
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            var flights = _context.Flights.ToList();
            return View(flights);
        }

        public ActionResult CreateFlight()
        {
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateFlight(Flight flight)
        {
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }



            if (!ModelState.IsValid)
                {
                    return View(flight);
                }

                if (_context.Flights.Any(f => f.FlightNumber == flight.FlightNumber))
                {
                    ModelState.AddModelError("", "A flight with the same flight number already exists.");
                    return View(flight);
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();

                return RedirectToAction("FlightsList");
            
        }

        public ActionResult EditFlight(int id)
        {
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (Session["Id"] == null || (UserRole)Session["Role"] != UserRole.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
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