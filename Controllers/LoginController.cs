using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Data_Access.Filters;
using User_Data_Access.Models;
using User_Data_Access.ViewModels;

namespace User_Data_Access.Controllers
{
    public class LoginController : Controller
    {

        public readonly SignInManager<IdentityUser> signInManager;
        public readonly UserManager<IdentityUser> userManager;
        public LoginController(SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("create", "RegisterInfo");
            }
            return View();
        }



        [HttpPost]
        [NoCache]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.username, model.password, model.RememberMe, lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    return RedirectToAction("create", "RegisterInfo");
                }
                else
                {
                    ModelState.AddModelError("","Incorrect email or password");
                }
            }
            return View("Login");
        }



        [HttpPost]
        [NoCache]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.email,
                    UserName = model.username
                };
                var result = await userManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("","Invalid");
                    return View("Register");
                }
            }
            return View("Register");
        }





    }
}
