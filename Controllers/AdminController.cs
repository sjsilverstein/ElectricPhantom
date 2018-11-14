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
        //Method takes in an int and checks if the currently logged 
        //in user has a user level => some int. If it does then
        //we should continue with whatever we are doing. 
        //If not we should set some controls as to what should happen.
        public bool CheckLoggedUserLevel(int checkLoggedUserLevelIs){
            // Check if the user is logged into the system if not return false.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return false;
            }
            //Check if loggin in user is UserLevel < checkLevelIs
            //If it is return false.
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);            
            if(loggedUser.UserLevel < checkLoggedUserLevelIs){
                return false;
            }

            return true;
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
            List<Unit> unitList = _context.Units.Include(u => u.Size).Include(u=>u.Item).ThenInclude(i =>i.ItemCatagory).ToList();
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
        //Creates a new Style of Item
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
        //Brings up an Item Style Page to Edit
        [HttpGet]
        [Route("Style/{itemId}")]
        public IActionResult Style(int itemId){
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

            //Query for Selected Item Style
            Item editStyle = _context.Items.Include(i =>i.ItemCatagory).SingleOrDefault(i => i.ItemId == itemId);
            ViewBag.EditItem = editStyle;

            return View("ItemStyle");
        }
        //Edits Item Style
        [HttpPost]
        [Route("UpdateItemStyle")]
        public IActionResult UpdateItemStyle(int itemId, string itemName, float price, string description){
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
            //Query for Selected Unit
            Item editItem = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
            //Make Changes to the Unit Inventory Count
            editItem.ItemName = itemName;
            editItem.Price = price;
            editItem.Description = description;
            
            editItem.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Style", new{ editItem.ItemId });
        }
        //Deletes an Item Style
        [HttpPost]
        [Route("DeleteStyle")]
        public IActionResult DeleteStyle(int itemId){

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

            //Delete selected Style
            Item RetrievedItem = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
            _context.Items.Remove(RetrievedItem);
            _context.SaveChanges();

            return RedirectToAction("CatalogAdmin", "Admin");
        }
        //Creates new Inventory of a Unit
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
        //Brings up Unit Inventory Page
        [HttpGet]
        [Route("UnitInventory/{unitId}")]
        public IActionResult UnitInventory(int unitId){
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

            //Query for Selected Unit
            Unit editUnit = _context.Units.Include(u => u.Size).Include(u=>u.Item).ThenInclude(i =>i.ItemCatagory).SingleOrDefault(u => u.UnitId == unitId);
            ViewBag.EditUnit = editUnit;

            return View("UnitInventory");
        }
        //Edits Inventory Count of a Unit
        [HttpPost]
        [Route("SetUnitCount")]
        public IActionResult SetUnitCount(int unitId, int count){
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
            //Query for Selected Unit
            Unit editUnit = _context.Units.SingleOrDefault(u => u.UnitId == unitId);
            //Make Changes to the Unit Inventory Count
            editUnit.Inventory = count;
            editUnit.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("UnitInventory", new{ editUnit.UnitId});
        }
        [HttpPost]
        [Route("DeleteInventory")]
        public IActionResult DeleteInventory(int unitId){

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
            Unit RetrievedUnit = _context.Units.SingleOrDefault(u => u.UnitId == unitId);
            _context.Units.Remove(RetrievedUnit);
            _context.SaveChanges();

            return RedirectToAction("CatalogAdmin", "Admin");
        }

    }
}