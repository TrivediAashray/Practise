using System.ComponentModel.DataAnnotations;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class UserRole // This class contains relation between the users and The associated roles
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Enter Role Id's")]
        public int RoleId { get; set; }      
    }
}
