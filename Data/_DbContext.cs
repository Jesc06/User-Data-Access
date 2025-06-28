
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
