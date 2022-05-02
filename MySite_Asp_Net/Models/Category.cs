using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite_Asp_Net.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        // робимо властивості virtual щоб proxies змогло їх зчитати.
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            this.Products = new List<Product>();
        }
    }
}
