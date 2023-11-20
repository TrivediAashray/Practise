using EmployeApiWithAuthentication_Authorization.Interfaces;
using EmployeApiWithAuthentication_Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeApiWithAuthentication_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AuthController(IAuthService authService)
        {
            _authService = authService;
           
        }


        // Login Method to get the Required Token 
        [HttpPost("Login")]
        public string Login(LoginRequest loginRequest)
        {
            try
            { 
                var token = _authService.Login(loginRequest);
           
                //_logger.Info(loginRequest.Username + " "+"has Logged in ");
                return token;
                
               
            }
            catch (Exception ex)
            {
                _logger.Info($"innerException : {ex.InnerException}");
                _logger.Info(ex.Message);
                return null;
                
            }


        }


        //To Get All the Users

        [HttpGet("GetUser")]
       
        public List<User> GetAllUsers()
        {
            var gtUser = _authService.GetAllUsers();
            return gtUser;
        }
        // For Adding a New User,They Should have Admin Rights
        [HttpPost("AddUser")]
        [Authorize(Roles = "Admin")]
        public User AddUser([FromBody] User user)
        {
            var addeduser = _authService.AddUser(user);
           
            return addeduser;
        }
        //To Edit the User Details ,They Should have Admin Rights

        [HttpPut("PutUser/{id}")]
        [Authorize(Roles = "Admin")]
        public User UpdateUser(User user, int id) 
        {
            if(user.Id == id)
            {
                var updateuser = _authService.UpdateUser(user);
                return updateuser;
            }
            else
            {
                return null;
            }
        
        }
        //To Delete the User ,They Should have Admin Rights

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public bool DeleteUser(int id)
        {
            try
            { 
                var deluser = _authService.DeleteUser(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
                return false;
            }
        }

        // For Adding a New Role,They Should have Admin Rights 
        [HttpPost("AddRole")]
        [Authorize(Roles = "Admin")]

        public Role AddRole( [FromBody] Role role)
        {
            var addRole = _authService.AddRole(role);
            return addRole; 
        }
        //To Get All the Role Details
        [HttpGet("GetAllRole")]
        public List<Role> GetAllRoles()
        {
            var gtRole = _authService.GetAllRole();
            return gtRole;
        }
        //To Edit Role Details ,They Should have Admin Rights

        [HttpPut("PutRole/{id}")]
        [Authorize(Roles = "Admin")]
        public Role UpdateRole(Role role, int id)
        {
            if (role.Id == id)
            {
                var updaterole = _authService.UpdateRole(role);
                return updaterole;
            }
            else
            {
                return null;
            }

        }
        //To Delete a Role ,They Should have Admin Rights

        [HttpDelete("DeleteRole/{id}")]
        [Authorize(Roles = "Admin")]
        public bool DeleteRole(int id)
        {
            try
            {
                var delrole = _authService.DeleteRole(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        //For Assigning Role to the User, They Should have Admin Rights
        [HttpPost("assignRole")]
        [Authorize(Roles = "Admin")]
        public bool AssignRoleToUser([FromBody] AddUserRole addUserRole)
        {
            var addedUserRole = _authService.AssignRoleToUser(addUserRole);
            return addedUserRole;
        }
    }
}
