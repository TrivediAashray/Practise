using System.ComponentModel.DataAnnotations;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class User // This is a user Class
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name can not be Empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Username can not be Empty")]
       
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not be Empty")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "User Status can not be Empty")]
        public bool IsActive {  get; set; }
    }
}
