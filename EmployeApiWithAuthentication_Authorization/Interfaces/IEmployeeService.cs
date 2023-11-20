using EmployeApiWithAuthentication_Authorization.Models;

namespace EmployeApiWithAuthentication_Authorization.Interfaces
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployee();

        public Employee GetEmployeeById(int id);
        public Employee AddEmployee(Employee employee);

        public Employee UpdateEmployee(Employee employee);

        public bool DeleteEmployee(int Id);
    }
}
