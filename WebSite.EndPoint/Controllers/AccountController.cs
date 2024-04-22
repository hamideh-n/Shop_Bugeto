using Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using WebSite.EndPoint.Models.ViewModels.User;
using WebSite.EndPoint.Services;

namespace WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public EmailService _emailService { get; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = new EmailService();
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
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email,
                PhoneNumber = register.PhoneNumber
            };
            var result = _userManager.CreateAsync(user, register.Password).Result;
            if (result.Succeeded)
            {
                var token=_userManager.GenerateEmailConfirmationTokenAsync(user);
                string callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    UserId = user.Id
               ,
                    token = token
                }, protocol: Request.Scheme);
                string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                _emailService.Execute(register.Email, body, "تایید ایمیل");
                return RedirectToAction(nameof(DisplayEmail));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return View(register);
        }
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userManager.FindByNameAsync(model.Email).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(model);
            }
            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user,model.Password,model.IsPersistent,true).Result;
            if (result.Succeeded)
            {
              return Redirect(model.ReturnUrl);
            }
            return View(model);
        }
        public IActionResult LogOut()
        {
           _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ConfirmEmail(string UserId, string Token)
        {
         
            return View();
        }
        public IActionResult DisplayEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest();
            }
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
            {
                return View("Error");
            }

            var result = _userManager.ConfirmEmailAsync(user, Token).Result;
            if (result.Succeeded)
            {
                /// return 
            }
            else
            {

            }
            return RedirectToAction("login");
        }
    }
}
