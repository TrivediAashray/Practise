using EmployeApiWithAuthentication_Authorization.Contexts;
using EmployeApiWithAuthentication_Authorization.Interfaces;
using EmployeApiWithAuthentication_Authorization.Models;
using NLog;

namespace EmployeApiWithAuthentication_Authorization.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeDbContext _employeDbContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EmployeeService(EmployeDbContext employeDbContext)
        {
            _employeDbContext = employeDbContext;
        }

        //This Method is used to add a new Employee 
        public Employee AddEmployee(Employee employee)
        {
            try
            { 
                var Emp = _employeDbContext.Employees.Add(employee);
                if (Emp != null)
                {
                    _employeDbContext.SaveChanges();
                    _logger.Info("Employee Added Sucesfully");
                    return Emp.Entity;
                }
                else
                {
                    _logger.Info("Employe Not Added");
                    return null;

                }
            }
            catch(Exception ex)
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;
            }
        }

        // This Method takes the EmployeeId as Parameter and Deletes the Employe from the Table
        public bool DeleteEmployee(int Id)
        {
            try
            {  //To Check if the Id passed is of a valid 
                var emp = _employeDbContext.Employees.SingleOrDefault(x => x.Id == Id);

                //Checks Whther the emp variable contains any Employee related to the Id or not
                if(emp == null)
                {
                     _logger.Info("No Employee Found for the Given Id");
                    return false;
                }
                else
                {
                    _employeDbContext.Remove(emp);
                    _employeDbContext.SaveChanges();
                    _logger.Info("Employe Deleted of Id :" + Id);

                    return true;
                }

            }catch(Exception ex)
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return false;
            }
        }

        // To get All the Employee present in the Database
        public List<Employee> GetAllEmployee()
        {

            var emp = _employeDbContext.Employees.ToList();
            return emp;
        }

        // To get the Employee by Id
        public Employee GetEmployeeById(int id)
        {
            var emp =  _employeDbContext.Employees.SingleOrDefault(x => x.Id == id);
            return emp;
        }

        // To Update the Employee 
        public Employee UpdateEmployee(Employee employee)
        {
            try
            { 

                var emp = _employeDbContext.Employees.Update(employee);
                if(emp != null)
                { 
                    _employeDbContext.SaveChanges();
                    _logger.Info("Employe Details Updated of EmployeId " + " " + employee.Id);
                    return emp.Entity;
                }
                else
                {
                    _logger.Info("Employe Details Not Updated");
                    return null;
                }
            }
            catch(Exception ex) 
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;    
            }
        }
    }
}
