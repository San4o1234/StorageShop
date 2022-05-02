using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net
{
    public static class DefaultData    // статичні дефолтні дані для тесту програми.
    {
        public static void Initialize(StorageContext context)
        {
            if (!context.Categories.Any())
            {
                var prod = new List<Product>();
                prod.AddRange(context.Products.ToList());
                context.Categories.AddRange(
                    new Category { CategoryName = "Toy" },
                    new Category { CategoryName = "Food" }
                    );
                context.SaveChanges();
            }

            if (!context.Products.Any())    // якщо обєктів немає.
            {
                context.Products.AddRange(
                    new Product { ProductName = "Doll", Price = 120, 
                        Category = context.Categories.Where(c => c.CategoryName == "Toy").FirstOrDefault()},
                    new Product { ProductName = "Lego", Price = 230, 
                        Category = context.Categories.Where(c => c.CategoryName == "Toy").FirstOrDefault() },
                    new Product { ProductName = "Milk", Price = 28,
                        Category = context.Categories.Where(c => c.CategoryName == "Food").FirstOrDefault()
                    } );
                context.SaveChanges();
            }
            
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new Role { roleName = "admin" },
                                       new Role { roleName = "user"});
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var r = context.Roles.Where(r => r.roleName == "admin").FirstOrDefault();
                context.Users.Add(new User
                {
                    FirstName = "Admin",
                    Email = "admin@admin.com",
                    Password = "123123",
                    Role = r
                });
            }

            if (!context.Orders.Any())      // повинно мати дефолтні значення.
            {
                var pr = context.Products.Where(p => p.Id == 1).FirstOrDefault();
                var user = context.Users.Where(u => u.Id == 1).FirstOrDefault();
                context.Orders.Add(new Order { User = user, OrderDate = DateTime.Now, Products = new List<Product> { pr} });
                context.SaveChanges();
            }
           
        }

    }
}
