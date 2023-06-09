﻿using FlightBookingSystem;
using FlightBookingSystem.Models;
using FlightBookingSystem.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Flight_Booking_System.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController()
        {
            _context = new AppDbContext();
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(model);
                }

             

                if (model.Password != confirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View(model);
                }
                //  model.Role = (UserRole)Enum.Parse(typeof(UserRole), "User");


                //   model.Role=UserRole.User;
                model.Role = FlightBookingSystem.UserRole.User; // Set the default role as User


                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: User/Login
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
                // Create the authentication cookie
                FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);

                Session["Id"] = user.UserId;
                Session["Email"] = user.Email;
                Session["Role"] = user.Role;

               

               
            }
            return RedirectToAction("Index", "Home");







            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }



        [HttpGet]
        public ActionResult Profile()
        {
            int userId = Convert.ToInt32(Session["Id"]);

            User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            List<Booking> bookings = _context.Bookings.Where(b => b.UserId == userId).ToList();

            ViewBag.Bookings = bookings;

            return View(user);
        }

        // GET: User/Logout
        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear();
          //  Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        // GET: /User/CancelBooking/{id}
        public ActionResult CancelBooking(int id)
        {
            int userId;
            if (Session["Id"] == null || !int.TryParse(Session["Id"].ToString(), out userId))
            {
                return RedirectToAction("Login");
            }

            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id && b.UserId == userId);
            if (booking == null)
            {
                return HttpNotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction("Profile", "User");
        }









        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
