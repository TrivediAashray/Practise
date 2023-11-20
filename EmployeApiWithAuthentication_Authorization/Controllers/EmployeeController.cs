using EmployeApiWithAuthentication_Authorization.Interfaces;
using EmployeApiWithAuthentication_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeApiWithAuthentication_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET 
        [HttpGet("GetAllEmploye")]
        [Authorize(Roles = "GetUser")]

        public List<Employee> GetAllEmployee()
        {
           var emp = _employeeService.GetAllEmployee();
            return emp;
        }

        // GET BY ID
        [HttpGet("GetEmployeById/{id}")]
        [Authorize(Roles = "GetUser")]
        public  Employee GetEmployeeById(int id)
        {
            var empid = _employeeService.GetEmployeeById(id);
            return empid;
        }

        // POST 
        [HttpPost("AddEmploye")]
        [Authorize(Roles = "PostUser")]
        public Employee AddEmployee([FromBody] Employee employee)
        {
            var addemp = _employeeService.AddEmployee(employee);
            return addemp;
        }

        // PUT 
        [HttpPut("PutEmploye/{id}")]
        [Authorize(Roles = "PutUser")]
        public Employee UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return null;
            }
            else
            { 
                var emp = _employeeService.UpdateEmployee(employee);
            
                 return emp;
            }
        }   

        // DELETE 
        [HttpDelete("DeleteEmploye/{id}")]
        [Authorize(Roles = "DeleteUser")]
        public bool DeleteEmployee(int Id)
        {
            var delEmp = _employeeService.DeleteEmployee(Id);
            return delEmp;
        }
    }
}
