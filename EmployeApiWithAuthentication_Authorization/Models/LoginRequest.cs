using System.ComponentModel.DataAnnotations;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class LoginRequest // This Class is meant to check the login request
    {
       
        public string Username {  get; set; }

        
        public string Password { get; set; }
    }
}
