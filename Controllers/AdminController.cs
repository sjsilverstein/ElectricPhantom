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
    public class AdminController : Controller
    {
        //Connection to Database
        private ElectricPhantomContext _context;
        public AdminController(ElectricPhantomContext context){
            _context = context;
        }

        //Delete User
        [HttpGet]
        [Route("Delete/{userId}")]
        public IActionResult Delete(int userId){

            // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Login");
            }
            //Check if loggin in user is a SuperAdmin UserLevel == 9
            //If not log them out.
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);            
            if(loggedUser.UserLevel != 9){
                return RedirectToAction("Logoff", "Login");
            }

            //Delete selected User
            User RetrievedUser = _context.Users.SingleOrDefault(user => user.UserId == userId);
            _context.Users.Remove(RetrievedUser);
            _context.SaveChanges();

            //Redirect Admin to List of all Users.
            return RedirectToAction("ListUsers", "Login");
        }
        //Admin Edit User Page
        [HttpGet]
        [Route("AdminEdit/{userId}")]
        public IActionResult AdminEdit(int userId){
            // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Login");
            }
            //Check if loggin in user is a SuperAdmin UserLevel == 9
            //If not log them out.
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);            
            if(loggedUser.UserLevel != 9){
                return RedirectToAction("Logoff", "Login");
            }
            
            //Query for Selected User
            User editUser = _context.Users.SingleOrDefault(u => u.UserId == userId);
            ViewBag.EditUser = editUser;


            return View("AdminEditUser");

        }
        //Set Users Level
        [HttpPost]
        [Route("AdminSetUserLevel")]
        public IActionResult AdminSetUserLevel(int userId, int newUserLevel){
            // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Login");
            }
            //Check if loggin in user is a SuperAdmin UserLevel == 9
            //If not log them out.
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);            
            if(loggedUser.UserLevel != 9){
                return RedirectToAction("Logoff", "Login");
            }
            
            //Query for Selected User
            User editUser = _context.Users.SingleOrDefault(u => u.UserId == userId);

            //Make Changes to the UserLevel
            editUser.UserLevel = newUserLevel;
            editUser.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            ViewBag.EditUser = editUser;

            return RedirectToAction("AdminEdit", new{ editUser.UserId } );
        }

        [HttpGet]
        [Route("CatalogAdmin")]
        public IActionResult CatalogAdmin(){
             // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Login");
            }
            //Check if loggin in user is a SuperAdmin UserLevel == 9
            //If not log them out.
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);            
            if(loggedUser.UserLevel != 9){
                return RedirectToAction("Logoff", "Login");
            }

            //Query for All Catagories
            List<Catagory> catagoryList = _context.Catagories.ToList();
            ViewBag.catagories = catagoryList;
            //Query for All Sizes
            List<Size> sizesAvailible = _context.Sizes.ToList();
            ViewBag.sizes = sizesAvailible;
            //Query for All Items including the catagor which they belong to.
            List<Item> styles = _context.Items.Include(i=>i.ItemCatagory).ToList();
            ViewBag.itemList = styles;
            //Query for Inventory of all Units
            List<Unit> unitList = _context.Units.Include(u=>u.Item).ThenInclude(i =>i.ItemCatagory).ToList();
            ViewBag.inventory = unitList;  
            
            return View("CatalogAmin");
        }
        
        [HttpPost]
        [Route("CreateCatagory")]
        public IActionResult CreateCatagory(CatagoryViewModel newCatagory){
            if(ModelState.IsValid){
                
                Catagory addCatagory = new Catagory{
                    CatagoryName = newCatagory.CatagoryName
                };

                _context.Add(addCatagory);
                _context.SaveChanges();
            }
            return RedirectToAction("CatalogAdmin", "Admin");
        }
        [HttpPost]
        [Route("CreateSize")]
        public IActionResult CreateSize(SizeViewModel newSize){
            if(ModelState.IsValid){

                Size addSize = new Size {
                    SizeName = newSize.SizeName
                };

                _context.Add(addSize);
                _context.SaveChanges();
            }
            return RedirectToAction("CatalogAdmin", "Admin");
        }
        [HttpPost]
        [Route("CreateItem")]
        public IActionResult CreateItem(ItemViewModel newItem){
            if(ModelState.IsValid){

                Item addItem = new Item {
                    ItemName = newItem.ItemName,
                    Description = newItem.Description,
                    Price = newItem.Price,
                    CatagoryId = newItem.CatagoryId
                };

                _context.Add(addItem);
                _context.SaveChanges();
            }
            return RedirectToAction("CatalogAdmin", "Admin");
        }
        [HttpPost]
        [Route("CreateInventory")]
        public IActionResult CreateInventory(InventoryViewModel newInventory){
            if(ModelState.IsValid){

                Unit addUnits = new Unit {
                    Inventory = newInventory.Inventory,
                    SizeId = newInventory.SizeId,
                    ItemId = newInventory.ItemId
                };

                _context.Add(addUnits);
                _context.SaveChanges();

            }
            return RedirectToAction("CatalogAdmin", "Admin");
        }
    }
}