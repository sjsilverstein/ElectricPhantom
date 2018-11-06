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
    public class HomeController : Controller
    {
        //Connection to Database
        private ElectricPhantomContext _context;
        public HomeController(ElectricPhantomContext context){
            _context = context;
        }

        //Root
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View("Index");
        }   
    }
}
