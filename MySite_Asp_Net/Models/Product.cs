using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite_Asp_Net.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }        
        public virtual Category Category { get; set; }
        //public virtual Order Order { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Product()
        {
            this.Orders = new List<Order>();
        }

    }
}
