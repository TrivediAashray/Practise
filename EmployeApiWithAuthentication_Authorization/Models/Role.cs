using System.ComponentModel.DataAnnotations;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Role Name Can not be Empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role Description Can not be Empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Role Status can not be Empty")]
        public bool IsActive { get; set; }
    }
}
