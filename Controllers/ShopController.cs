using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElectricPhantom.Models;
using ElectricPhantom.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ElectricPhantom.Controllers
{
    public class ShopController : Controller
    {
        //Connection to Database
        private ElectricPhantomContext _context;
        public ShopController(ElectricPhantomContext context){
            _context = context;
        }
        // Root/Shop
        // Landing page for Shopping
        [HttpGet]
        [Route("Shop")]
        public IActionResult Shop(){
            //All Item Styles
            List<Item> allStyles = _context.Items.OrderBy(i => i.CatagoryId).ToList();
            ViewBag.AllStyles = allStyles;
            //All Catagories
            List<Catagory> allCata = _context.Catagories.ToList();
            ViewBag.AllCata = allCata;
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
