using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;     // для роботи з MVC
using Microsoft.EntityFrameworkCore;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    public class BuyController: Controller     // Базовий клас для всіх контроллерів.
    {
        StorageContext context;

        public BuyController(StorageContext cont)
        {
            this.context = cont;
        }
        
        [HttpGet]   // атрибут
        public IActionResult Buy(int? id)
        {
            if (id == null)
               return RedirectToAction("AllProducts","Product");      // повертає на сторінку Index
            
            ViewBag.ProductId = id;             // повертає з форми 1 обєкт і називаєм ProductId.
            var product = context.Products.Where(p => p.Id == id).FirstOrDefault();
            return View();  // повертаєм дані.
        }

        [HttpPost]    // атрибут який задає зберігання в БД.
        public IActionResult Buy(Order order)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.Where(u => u.Email == order.User.Email).FirstOrDefault();
                var prod = context.Products.Where(p => p.Id == order.Id).FirstOrDefault();
                Order order2 = new Order() { User = user, OrderDate = DateTime.Now, 
                    Products = new List<Product> { prod} };                                
                context.Orders.Add(order2);

                context.SaveChanges();
                return RedirectToAction("AllProducts", "Product");
            }

            return View();
        }

        // https://github.com/San4o1234/MyProjectGym.git


    }
}
