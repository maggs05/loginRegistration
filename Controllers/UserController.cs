using DbConnection;
using Microsoft.AspNetCore.Mvc;
using loginRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace loginRegistration.Controllers
{
    public class UserController: Controller{
        private bool IsEmailUnique(string EmailAddress){
            return DbConnector.Query($"Select id FROM users WHERE email='{EmailAddress}'").Count==0;
        }
        private HomePageUsers GetUsers(LoginUser logUser = null, RegisterUser regUser = null)
        {
            return new HomePageUsers()
            {
                Users = DbConnector.Query("SELECT * FROM users"),
                Login = logUser == null ? new LoginUser() : logUser,
                Register = regUser == null ? new RegisterUser() : regUser
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
                ModelState.AddModelError("email", "Email is already in use");

            }
            if(ModelState.IsValid){
                PasswordHasher<RegisterUser>hasher= new PasswordHasher<RegisterUser>();
                string hashedPass = hasher.HashPassword(user, user.Password);

                string query= $@"INSERT INTO users (FirstName, LastName, EmailAddress, Password, created_at, updated_at) VALUES('{user.FirstName}','{user.LastName}','{user.EmailAddress}','{hashedPass}',NOW(),NOW()";

                DbConnector.Execute(query);
                return Json(new{
                    success=true,
                    newUser=user
                });
            }
             return View("Index", GetUsers(null, user));
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult Login(LoginUser user){
            return RedirectToAction("Success");
                 if(IsEmailUnique(user.LogEmail))
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
            }
            else
            {
                string hashed = (string)DbConnector.Query($"SELECT password FROM users WHERE email='{user.LogEmail}'")[0]["password"];
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

                
                    if(hasher.VerifyHashedPassword(user, hashed, user.LogPassword) == 0)
                    {
                        ModelState.AddModelError("LogEmail", "Invalid Email/Password");
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

        
