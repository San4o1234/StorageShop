using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite_Asp_Net.Models
{
    public class User
    {
        public int Id { get; set; }   // якщо використовуєм від Identity то Id, email не потрібно.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public User()
        {
            this.Orders = new List<Order>();
        }
    }
}
