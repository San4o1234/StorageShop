using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    public class ProductController : Controller
    {
        StorageContext context;
        public ProductController(StorageContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Category cat = context.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                if(cat != null)
                {
                    context.Products.Add(new Product
                    {
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Category = cat
                    });
                    context.SaveChanges();
                    return RedirectToAction("AllProducts", "Product");
                }
                ModelState.AddModelError("", "CategoryName Empty or Wrong");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
                return RedirectToAction("AllProducts", "Product");
            var prod = context.Products.FirstOrDefault(p => p.Id == id);
            return View(prod);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                    context.Products.Update(product);
                    context.SaveChanges();
                    return RedirectToAction("AllProducts", "Product");               
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult RemoveProduct(int? id)
        {
            if (id == null)
                return RedirectToAction("AllProducts", "Product");
            var prod = context.Products.FirstOrDefault(p => p.Id == id);
            return View(prod);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult RemoveProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("AllProducts", "Product");
        }

        public IActionResult AllProducts()
        {
            return View(context.Products.ToList());      // передаємо у View дані з БД (передаєм model).
        }
    }
}
