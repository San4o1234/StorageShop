using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;      // для автентифікації.
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using MySite_Asp_Net.Models;


namespace MySite_Asp_Net.Controllers
{
    public class AccountController:Controller
    {
        private StorageContext context;
        public AccountController(StorageContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.FirstOrDefault(u => u.Email == model.Email);
                if(user == null)
                {
                    context.Users.Add(new User
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Password = model.Password, 
                        Role = context.Roles.Where(r => r.roleName == "user").FirstOrDefault()
                    });
                    context.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }                
            }
            return View(model);     // якщо модель не валідна то повертаєм view i модель для перевірки користувача.
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.Include(u=>u.Role).FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if(user != null)
                {
                    Auth(user);  // автентифікуємось по Email.
                    return RedirectToAction("ViewStart","MainPage");
                }
                ModelState.AddModelError("", "Empty or Wrong Email or Pass");
            }
            return View();
        }
        private void Auth(User user)
        {
            // створюєм claim щоб дані загнати в cookie
            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.roleName) };
            // провірка якщо користувач буде зареєстрований тоді зможе виконувати доступні ф-ції.
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        // [HttpPost]         //якщо Logout не працює, то забрати [HttpPost
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }



    }
}
