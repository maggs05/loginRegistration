using Microsoft.AspNetCore.Mvc;
using loginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace loginRegistration.Controllers
{
    public class UserController: Controller{
        private LoginRegContext _context;
 
        public UserController(LoginRegContext context)
    {
        
        _context = context;
    }
      
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            
            return View();
        }
        [HttpPost]
        [Route("Create")] 
        public IActionResult Create(NewUser user){
            if(_context.users.Where(u=>u.EmailAddress==user.EmailAddress).ToList().Count()>0){
                ModelState.AddModelError("EmailAddress","Email already exists!");
            return View("Index");
            }
            
            if(ModelState.IsValid){
            PasswordHasher<User>hasher=new PasswordHasher<User>();
            user.Password=hasher.HashPassword(user,user.Password);
            _context.users.Add(user);
            _context.SaveChanges();

            return Json(user);
            }
        return View("Index");
        }      
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            if(_context.users.Where(u=>u.EmailAddress==user.LogEmail).ToList().Count()==0){
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
            
            }
            else{
                User ToCheck=_context.users.SingleOrDefault(u=>u.EmailAddress==user.LogEmail);
                PasswordHasher<LoginUser>hasher= new PasswordHasher<LoginUser>();
                if(hasher.VerifyHashedPassword(user,ToCheck.Password,user.LogPassword)==0){
                    ModelState.AddModelError("LogEmail","Invalid Email/Password");
                }
            }
                if(ModelState.IsValid){
                     User ToLog=_context.users.SingleOrDefault(u=>u.EmailAddress==user.LogEmail);
                    HttpContext.Session.SetInt32("id",(int)ToLog.User_Id);
                    return Json(user);
                }
                return View("Index");

            }
        }               
    }
           
              


        
