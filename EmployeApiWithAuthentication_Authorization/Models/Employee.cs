using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeApiWithAuthentication_Authorization.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Employe Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter Age")]
        public string Age { get; set; }
        [Required(ErrorMessage ="Please Enter Company Name")]
        public string Company { get; set; }

    }
}
