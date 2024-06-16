using System.ComponentModel.DataAnnotations;

namespace ApisElHierroJWT.Models
{
    

    public class LoginUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }

   
}