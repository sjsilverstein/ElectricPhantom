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
            List<Item> allStyles = _context.Items.OrderBy(i => i.ItemName).ToList();
            ViewBag.AllStyles = allStyles;
            //All Catagories
            List<Catagory> allCata = _context.Catagories.OrderBy(c => c.CatagoryName).ToList();
            ViewBag.AllCata = allCata;
            return View("Shop");
        }
        // Management Page for the shop. Only allow users with user level == 9 to access.
        [HttpGet]
        [Route("Shop/Style/{itemId}")]
        public IActionResult ShopStyle(int itemId){
            Item getItem = _context.Items.Include(i => i.ItemCatagory).SingleOrDefault(i => i.ItemId == itemId);
            ViewBag.Style = getItem;
            List<Unit> getStyleInventoryByItemId = _context.Units.Where(u => u.ItemId == itemId).Include(u => u.Size).Include(u => u.Item).ToList();
            ViewBag.StyleInventory = getStyleInventoryByItemId;
            return View("ShopStyle");
        }
        //Add Unit to Cart in Session
        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel itemToAdd){
            Console.WriteLine("***XOXOXOXOXOXOXOXOXOXOXOXOXOOXOXOXOXOXOXOXOXOXOXOXOXOXOXO Made it to Add to Cart ****");
            return RedirectToAction("ShopStyle", new {itemId=itemToAdd.ItemId});
        }

    }
}
