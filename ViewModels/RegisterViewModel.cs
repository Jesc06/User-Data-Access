using System.ComponentModel.DataAnnotations;

namespace User_Data_Access.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Email must required")]
        [EmailAddress]
        public string email { get; set; }


        [Required(ErrorMessage = "Username must required")]
        public string username { get; set; }


        [Required(ErrorMessage = "Password must required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "The {0} must be at {2} and at max {1} characters")]
        [Compare("Confirmpassword", ErrorMessage = "Password does not match")]
        public string password { get; set; }


        [Required(ErrorMessage = "Password must required")]
        public string Confirmpassword { get; set; }

    }
}
