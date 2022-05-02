using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    // для роботи з identity.
using MySite_Asp_Net.Models;

namespace MySite_Asp_Net
{
    public class StorageContext: DbContext  // наслідуємося від DbContext  щоб доступатися до підключення БД
    {
        public StorageContext(DbContextOptions<StorageContext> options): base(options)
        {
            // якщо є БД то дефолних значень не потрібно.
            Database.EnsureCreated();   // властивість додає дані в БД.
        }

        // робимо властивості virtual щоб proxies змогло їх зчитати.
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
