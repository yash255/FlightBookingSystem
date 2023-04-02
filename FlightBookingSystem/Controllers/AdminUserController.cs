using FlightBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly Resource.AppDbContext _context;

        public AdminUserController()
        {
            _context = new Resource.AppDbContext();
        }
        // GET: AdminUser
        // CRUD operations for users

        public ActionResult UsersList()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        /*public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            user.Role = UserRole.User;
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }*/

        public ActionResult EditUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return HttpNotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;


            _context.SaveChanges();

            return RedirectToAction("UsersList");
        }

        public ActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("UsersList");
        }
    }
}
    
