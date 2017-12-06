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
        [HttpGet]
        [Route("")]
        public IActionResult Index(RegisterUser user){
            if(!IsEmailUnique(user.EmailAddress)){
                ModelState.AddModelError("email", "Email is already in use");

            }
            if(ModelState.IsValid){
                PasswordHasher<RegisterUser>hasher= new PasswordHasher<RegisterUser>();
                string hashedPass = hasher.HashPassword(user, user.Password);
            }
             return View();
        }
        [HttpPost]
        [Route("submit")]
        public IActionResult Register(RegisterUser model){
            if(ModelState.IsValid){



                };
                return RedirectToAction("Success");

            }
            return View("Index");
        }
    }
}
