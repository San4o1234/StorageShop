using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    [Authorize]
    public class ProfileController: Controller
    {
        StorageContext context;
        public ProfileController(StorageContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult ProfileUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                // шуукаємо user з властивості User.Identity
                User user = context.Users.Where(s => s.Email == User.Identity.Name).FirstOrDefault();
                return View(user);
            }
            return View();            
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditUser()
        {
                User user = context.Users.Where(s => s.Email == User.Identity.Name).FirstOrDefault();
                return View(user);       
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            context.Update(user);
            context.SaveChanges();
            return RedirectToAction("ProfileUser", "Profile");
        }
    }
}
