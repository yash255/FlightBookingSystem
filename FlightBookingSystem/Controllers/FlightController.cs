using FlightBookingSystem.Resource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly Resource.AppDbContext _context;

        public FlightController()
        {
            _context = new AppDbContext();
        }

        public ActionResult SearchResult()
        {
            return View();
        }


        public ActionResult SearchResult(string departureCity, string arrivalCity, DateTime date)
        {
            var flights = _context.Flights
                .Include(f => f.DepartureCity)
                .Include(f => f.ArrivalCity)
                .Where(f => f.DepartureCity.Contains(departureCity) && f.ArrivalCity.Contains(arrivalCity) && f.DepartureTime.Date == date.Date);

            if (flights.Any())
            {
                return View(flights);
            }

            ViewBag.Message = "No flights found for the selected criteria.";
            return View();
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string arrivalCity, string departureCity, DateTime? date)
        {
            if (string.IsNullOrEmpty(arrivalCity) || string.IsNullOrEmpty(departureCity) || date == null)
            {
                ModelState.AddModelError("", "Please fill all fields.");
                return View("Index");
            }

            var flights = _context.Flights
            
            .Where(f => f.DepartureCity.Contains(departureCity) && f.ArrivalCity.Contains(arrivalCity) && DbFunctions.TruncateTime(f.DepartureTime) == DbFunctions.TruncateTime(date))
            .ToList();



            if (flights.Count == 0)
            {
                ModelState.AddModelError("", "No flights found for the selected criteria.");
            }

            return View("SearchResult", flights);
        }
    }

}