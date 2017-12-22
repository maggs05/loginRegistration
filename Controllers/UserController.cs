using System;
using DbConnection;
using Microsoft.AspNetCore.Mvc;
using loginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace loginRegistration.Controllers
{
    public class UserController: Controller{
        private bool IsEmailUnique(string EmailAddress){
            return DbConnector.Query($"Select id FROM users WHERE EmailAddress='{EmailAddress}'").Count==0;
        }
        private HomePageUsers GetUsers(LoginUser LoginUser = null, RegisterUser RegisterUser = null)
        {
            return new HomePageUsers()
            {
                Users = DbConnector.Query("SELECT * FROM users"),
                LoginUser = LoginUser == null ? new LoginUser() : LoginUser,
                RegisterUser = RegisterUser == null ? new RegisterUser() : RegisterUser
            };
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            
            return View(GetUsers());
        }
        [HttpPost]  
        [Route("Register")]
        public IActionResult Register(RegisterUser user){
            if(!IsEmailUnique(user.EmailAddress)){
                ModelState.AddModelError("EmailAddress", "Email is already in use");

            }
            if(ModelState.IsValid){
                PasswordHasher<RegisterUser>hasher= new PasswordHasher<RegisterUser>();
                string hashedPass = hasher.HashPassword(user, user.Password);

                string query= $"INSERT INTO users (FirstName, LastName, EmailAddress, Password, created_at, updated_at) VALUES ('{user.FirstName}','{user.LastName}','{user.EmailAddress}','{hashedPass}',NOW(),NOW())";

                DbConnector.Execute(query);
                return Json(new{
                    success=true,
                    newUser=user
                });
            }
             return View("Index", GetUsers(null, user));
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            if(IsEmailUnique(user.LogEmail))
            {
                ModelState.AddModelError("LogEmail","Invalid Email/Password");
            }
            else
            {
                string hashed = (string)DbConnector.Query($"SELECT Password FROM users WHERE EmailAddress='{user.LogEmail}'")[0]["Password"];
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                    if(0 == hasher.VerifyHashedPassword(user,hashed,user.LogPassword))                
                    // if(hasher.VerifyHashedPassword(user, hashed, user.LogPassword) != 0)
                    {
                        ModelState.AddModelError("LogEmail","Invalid Email/Password");
                    }

            }

            if(ModelState.IsValid)
            {
                return Json(new {
                    success=true,
                    newUser=user
                });
            }
            return View("Index", GetUsers(user, null));
        }
    }
}

        
