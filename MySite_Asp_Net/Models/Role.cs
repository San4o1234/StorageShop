using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MySite_Asp_Net.Models
{   
    public class Role
    {
        public int Id { get;set;}
        public string roleName { get; set; }
        // робимо властивості virtual щоб proxies змогло їх зчитати.
        public virtual List<User> Users { get; set; }
        public Role()
        {
            this.Users = new List<User>();
        }
    }
}
