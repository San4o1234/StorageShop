using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite_Asp_Net.Controllers
{
    public class MainPageController : Controller
    {
        public IActionResult ViewStart()
        {
            return View();
        }
    }
}
