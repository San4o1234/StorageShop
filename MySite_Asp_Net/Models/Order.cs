using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;     // дозволяє задавати identity і інше в таблицях SQL.


namespace MySite_Asp_Net.Models
{
    public class Order
    {
       // [DatabaseGeneratedAttribute( DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual User User { get; set; }
        // робимо властивості virtual щоб proxies змогло їх зчитати.
        //public virtual Product Product { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Order()
        {
            this.Products = new List<Product>();
        }
    }
}
