# User-Data-Access
User data access using Asp.NetCore Identity 
 
<br> 
<br>
  
### 1. Create Model

```csharp
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace User_Data_Access.Models
{
    public class StudentInformation
    {
        [Key]
        public int? Id { get; set; }

        public string? name { get; set; }
   
        public string? middlename { get; set; }
 
        public string? lastname { get; set; }


        //Foreign Key to IdentiyUser
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }



    }
}

```

<br>
<br>

### 2. Create _DbContext file

```csharp

using User_Data_Access.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace User_Data_Access.Data
{
    public class _DbContext : IdentityDbContext<IdentityUser>
    {
        public _DbContext(DbContextOptions options) : base (options) { }
        public DbSet<StudentInformation> StudentInformation { get; set; }
    }
}

```

<br>

### add-migration
### update-database

<br>
<br>


### 3. Logic for Add data with UserData

```csharp

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

```

<br>
<br>


### 4. Logic for filter data with UserData

```csharp
        public async Task<IActionResult> table()
        {
            var userId = userManager.GetUserId(User);
            List<StudentInformation> studentInfo = await context.StudentInformation
                .Where(info => info.UserId == userId)
                .ToListAsync();
            return View(studentInfo);
        }
```


<br>
<br>


### 5. Logic for display custom data with UserData

```csharp
  var userId = userManager.GetUserId(User);//current already authenticated login userId
  var user = await userManager.FindByIdAsync(userId);

  ViewBag.Username = user.Email;
```




<br>
<br>


### 5. Overall Controllers


```csharp
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




    }
}

```


