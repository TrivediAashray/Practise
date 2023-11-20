using System.ComponentModel.DataAnnotations;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class AddUserRole  //This is a clas for Adding Roles for a User
    {
        [Required(ErrorMessage ="Enter UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Enter Role Id's")]
        public List<int> RoleIds { get; set; }
    }
}
