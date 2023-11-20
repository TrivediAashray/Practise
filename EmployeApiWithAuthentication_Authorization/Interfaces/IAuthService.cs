using EmployeApiWithAuthentication_Authorization.Models;

namespace EmployeApiWithAuthentication_Authorization.Interfaces
{
    public interface IAuthService
    {
        //Methods For Users
        User AddUser(User user);
        List<User> GetAllUsers();

        User UpdateUser(User user);

        bool DeleteUser(int id);

        //Login Method
        string Login(LoginRequest loginRequest);

        //Methods for Roles
        Role AddRole(Role role);
        List<Role> GetAllRole();

        Role UpdateRole(Role user);

        bool DeleteRole(int id);

        //Method to Assign Roles to the User
        bool AssignRoleToUser(AddUserRole obj);
    }
}
