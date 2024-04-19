using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Models.ViewModels.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            User user = new User 
            {
            FullName=register.FullName,
            Email=register.Email,
            PhoneNumber=register.PhoneNumber
            };
           var result= _userManager.CreateAsync(user,register.Password).Result;
            if (result.Succeeded)
            {
               return RedirectToAction(nameof(Profile));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code,error.Description);
            }
            return View(register);
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
