using Domains.Models;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace LapShop.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string? returnUrl)
        {
            LoginModel model = new LoginModel();
            model.ReturnUrl = returnUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View("Login", model);
                
            }
          
            var result =await _signInManager.PasswordSignInAsync(model.Email,model.Password,true, true);
            if (result.Succeeded)
            {
                if(string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            return View();
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            } 
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View();
        }
        public async Task <IActionResult> LoginOut()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction("List","Home");
            return Redirect("~/");
        }
    }
}
