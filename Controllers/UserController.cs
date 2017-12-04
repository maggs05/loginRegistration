using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// using loginRegistration.Models;
// using Microsoft.AspNetCore.Identity;
namespace loginRegistration.Controllers{
    public class UserController: Controller{

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
    
             return View();
        }

        
    }
}
