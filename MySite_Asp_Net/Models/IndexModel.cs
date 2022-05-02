using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;      // для роботи Razor Pages.

namespace MySite_Asp_Net.Models
{
    public class IndexModel: PageModel      // PageModel - клас для функціоналу і роботи сторінок
    {
        public string SomeMsg { get; set; }

        public void OnPost()         // методи для передавання повідомлень на сторінку
        {
            SomeMsg = "Hello from Razor Pages" + DateTime.Now;
        }

    }
}
