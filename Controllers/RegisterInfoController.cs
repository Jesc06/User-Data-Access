using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Data_Access.Models;
using User_Data_Access.Filters;
using Microsoft.AspNetCore.Authorization;
using User_Data_Access.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace User_Data_Access.Controllers
{
    [Authorize]
    [NoCache]
    public class RegisterInfoController : Controller
    {

        public readonly _DbContext context;
        public readonly SignInManager<IdentityUser> signInManager;
        public readonly UserManager<IdentityUser> userManager;
        public RegisterInfoController(_DbContext _context, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager) 
        { 
            signInManager = _signInManager;
            userManager = _userManager;
            context = _context;
        }

      
        public async Task<IActionResult> create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = userManager.GetUserId(User);//current already authenticated login userId
            var user = await userManager.FindByIdAsync(userId);

            ViewBag.Username = user.Email;
          
            return View(); 
        }




        public async Task<IActionResult> table()
        {
            var userId = userManager.GetUserId(User);
            List<StudentInformation> studentInfo = await context.StudentInformation
                .Where(info => info.UserId == userId)
                .ToListAsync();
            return View(studentInfo);
        }




        [HttpPost]
        public async Task<IActionResult> create(StudentInformation model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = userManager.GetUserId(User);
                context.Add(model);
                await context.SaveChangesAsync();
               
                return RedirectToAction("table");
            }
            return View("create");
        }




        [HttpPost]
        public IActionResult GotoTable()
        {
            return RedirectToAction("table", "RegisterInfo");
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Login");
        }




    }
}
