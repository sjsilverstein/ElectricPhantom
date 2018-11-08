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

namespace ElectricPhantom.Controllers
{
    public class LoginController : Controller
    {
        //Connection to Database
        private ElectricPhantomContext _context;
        public LoginController(ElectricPhantomContext context){
            _context = context;
        }
        //Root
        [HttpGet]
        [Route("Login")]
        public IActionResult Index(){
            return View("Index");
        }
        //Register New User
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(RegistrationViewModel newUser){
            HttpContext.Session.Clear();

            if(ModelState.IsValid){
                //Check email is not in use.
                User emailChecker = _context.Users.SingleOrDefault(u => u.Email == newUser.Email);

                if(emailChecker != null){
                    //Error Here
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }
                
                User addUser = new User{
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Password = newUser.Password
                };

                //Hash that Password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                addUser.Password = Hasher.HashPassword(addUser, newUser.Password);

                // checked to give SuperAdmin
                List<User> AllUsers = _context.Users.ToList();
                if(AllUsers.Count() == 0){
                    addUser.UserLevel = 9;
                }
                else {
                    addUser.UserLevel = 1;
                }

                // add User to Database
                _context.Add(addUser);
                _context.SaveChanges();

                //Save the new User to Session and move them to Userlist
                List<User> thisUser = _context.Users.Where(u => u.Email == addUser.Email).ToList();
                HttpContext.Session.SetInt32("UserId", (int)thisUser[0].UserId);

                //Direct new User to ListUsers

                return RedirectToAction("Shop", "Shop");
            }

            return RedirectToAction("Index", "Login");
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login (LoginViewModel userToCheck){
            if (ModelState.IsValid){

                User IsUser = _context.Users.SingleOrDefault(u => u.Email == userToCheck.Email);
                if(IsUser != null){

                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(IsUser, IsUser.Password, userToCheck.Password)){
                        HttpContext.Session.SetInt32("UserId", (int)IsUser.UserId);

                        if(IsUser.UserLevel == 9 ){
                            return RedirectToAction("CatalogAdmin", "Admin");    
                        }
                        return RedirectToAction("ListUsers", "Login");
                    }
                }
            }
            return View("Index");
        }

        //Log off Clear Session
        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); 
        }

        //List of all Users
        [HttpGet]
        [Route("ListUsers")]
        public IActionResult ListUsers(){

             // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Login");
            }

            //List of all users
            List<User> userList = _context.Users.ToList();
            ViewBag.dashboard = userList;
            //Pull Logged in user
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);
            ViewBag.LoggedUser = loggedUser;
            
            return View("ListUsers"); 
        }
    }
}
