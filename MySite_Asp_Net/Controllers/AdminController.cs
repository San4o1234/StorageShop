using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        StorageContext context;
        public AdminController(StorageContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult AllUsers()
        {
            return View(context.Users.ToList());
        }
               

        [HttpGet]
        public IActionResult EditUser(int? id)
        {
            if (id == null)
                return RedirectToAction("AllUsers", "Admin");
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Role.roleName != "user" || user.Role.roleName != "admin")
                {
                    ModelState.AddModelError("", "Role must be user or admin");
                }
                context.Users.Update(user);
                context.SaveChanges();
                return RedirectToAction("AllUsers", "Admin");
            }

            return RedirectToAction("AllUsers", "Admin");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Role.roleName != "user" || user.Role.roleName != "admin")
                {
                    ModelState.AddModelError("", "Role must be user or admin");
                }
                context.Users.Add(new User
                {
                    FirstName = user.FirstName, LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber, Email = user.Email,
                    Password = user.Password, Role = user.Role
                });
                context.SaveChanges();
                return RedirectToAction("AllUsers", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult RemoveUser(int? id)
        {
            if(id == null)
                return RedirectToAction("AllUsers", "Admin");
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult RemoveUser(User user)
        {
            context.Remove(user);
            context.SaveChanges();
            return RedirectToAction("AllUsers", "Admin");
        }

        [HttpGet]
        public IActionResult DetailsUser(int? id)
        {
            if (id == null)
                return RedirectToAction("AllUsers", "Admin");
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }
    }
}
