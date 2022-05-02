using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net.Controllers
{
    public class CategoryController : Controller
    {
        StorageContext context;
        public CategoryController(StorageContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category { CategoryName = category.CategoryName };
                context.Categories.Add(cat);
                context.SaveChanges();
                return RedirectToAction("AllCategories", "Category");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CategoryDetails(int? id)
        {
            if(id == null)
                return RedirectToAction("AllCategories", "Category");

            var cat = context.Categories.FirstOrDefault(c => c.Id == id);
            return View(cat);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("AllCategories", "Category");
            }
            Category cat = context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(cat);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Update(category);
                context.SaveChanges();
                return RedirectToAction("AllCategories", "Category");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult RemoveCategory(int? id)
        {
            if(id == null)
             return RedirectToAction("AllCategories", "Category");

            var cat = context.Categories.FirstOrDefault(c => c.Id == id);
            return View(cat);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult RemoveCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("AllCategories", "Category");
        }

        public IActionResult AllCategories()
        {
            return View(context.Categories.ToList());
        }
    }
}
