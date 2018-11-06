using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElectricPhantom.Models;

namespace ElectricPhantom.Controllers
{
    public class ShopController : Controller
    {
        // Root/Shop
        // Landing page for Shopping
        [HttpGet]
        [Route("Shop")]
        public IActionResult Shop(){
            ViewBag.Example = "Hello World!";
            return View("Shop");
        }
        // Management Page for the shop. Only allow users with user level == 9 to access.
        [HttpGet]
        [Route("Shop/Manage")]
        public IActionResult ManageShop(){
            return View("ManageShop");
        }

    }
}
