using System.ComponentModel.DataAnnotations;

namespace User_Data_Access.ViewModels
{
    public class LoginViewModel
    {
      

        [Required(ErrorMessage = "Username must required")]
        public string username { get; set; }



        [Required(ErrorMessage = "Password must required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "The {0} must be at {2} and at max {1} characters")]
        public string password { get; set; }

  
        public bool RememberMe { get; set; }


    }
}
