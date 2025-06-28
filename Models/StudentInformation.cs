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
