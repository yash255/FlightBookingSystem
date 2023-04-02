using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext _context;

        public UserController()
        {
            _context = new AppDbContext();
        }



        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(model);
                }

                
            //    model.Role = UserRole.User;

                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

    

        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                Session["Id"] = user.Id;
                Session["Email"] = user.Email;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public ActionResult Profile(int id)
        {
            var user = _context.Users.Include("Bookings")
                              .FirstOrDefault(u => u.Id == id);


            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }

}