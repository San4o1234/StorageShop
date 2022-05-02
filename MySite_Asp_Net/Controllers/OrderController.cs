using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    public class OrderController : Controller
    {
        StorageContext context;     // змінна доступу до даних БД.

        public OrderController(StorageContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult AllOrders()
        {
            if (User.IsInRole("admin"))
            {
                return View(context.Orders.ToList());
            }
            var userOrders = context.Orders.Where(o => o.User.Email == User.Identity.Name).ToList();
            return View(userOrders);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("AllOrders","Order");

            ViewBag.OrderId = id;
            // для установки звязків використовуєм Proxies.
            var ord = context.Orders.Where(o => o.Id == id).FirstOrDefault();            
            return View(ord);   // передаєм в View модель Order з потрібним Id.
        }
        
        [Authorize(Roles ="admin, user")]
        [HttpGet]
        public IActionResult EditOrder(int? id)
        {
            Order order = context.Orders.Select(o => o).Where(o => o.Id == id).FirstOrDefault();
            return View(order);
        }
        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public IActionResult EditOrder(Order order)
        {
            context.Update(order);
            context.SaveChanges();
            return RedirectToAction("Details","Order");
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult RemoveOrder(int? id)
        {
            Order order = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            return View(order);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult RemoveOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("AllOrders", "Order");
        }
    }


}
