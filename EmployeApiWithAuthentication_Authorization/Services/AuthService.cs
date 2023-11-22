using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeApiWithAuthentication_Authorization.Contexts;
using EmployeApiWithAuthentication_Authorization.Interfaces;
using EmployeApiWithAuthentication_Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace EmployeApiWithAuthentication_Authorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly EmployeDbContext _employeDbContext;
        private readonly IConfiguration _configuration;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public AuthService(EmployeDbContext employeDbContext , IConfiguration configuration)
        {
            _employeDbContext = employeDbContext;
            _configuration = configuration;
            
        }
        //ROLE METHODS
        //This Method is used for Creating a new Role 
        public Role AddRole(Role role)
        {
            try
            {
                var addRole = _employeDbContext.Roles.Add(role);
                if(role != null)
                {  
                    
                    _employeDbContext.SaveChanges();
                    _logger.Info("Role Added"+ " "+ role.Name);
                    return addRole.Entity;
                 
                }
                else
                {
                    _logger.Info("There was some Error, Role Not Added");
                    return null;
                   
                }
            }
            catch (Exception ex) 
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;
            
            }
        }
        //To Get All Roles
        public List<Role> GetAllRole()
        {
            try
            {
                var roles = _employeDbContext.Roles.ToList();
                return roles;
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;
            }
        }
        //To Update Role Details
        public Role UpdateRole(Role role)
        {
            try
            {
                var updaterole = _employeDbContext.Roles.Update(role);
                if (updaterole != null)
                {
                    
                    _employeDbContext.SaveChanges();
                    _logger.Info("Role Updated Successfully"+" "+ role.Name);
                    return updaterole.Entity;
                 

                }
                else
                {
                    _logger.Info("There was some Error Updating the Role");
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
        //To Delete Roles
        public bool DeleteRole(int id)
        {
            try
            { 
                var role = _employeDbContext.Roles.SingleOrDefault(x => x.Id == id);
                if(role != null)
                {
                    _employeDbContext.Roles.Remove(role);
                    _employeDbContext.SaveChanges();
                    _logger.Info("Role Successfully Deleted"+ " "+role.Name);
                    return true;
                   
                }
                else
                {
                    _logger.Info("Error Deleting Role");
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return false;
              
            }
        }

        // USER METHODS
        //This Method is used for Creating a new User
        public User AddUser(User user)
        {
            try
            { 
                //To store the Password in an Encrypted Form 
                PasswordManager passwordManager = new PasswordManager();
                string hashedPassword = passwordManager.HashPassword(user.Password);
                //Replacing the Plain Text Password With Encrypted Password
                user.Password= hashedPassword;
            
                var addUser =   _employeDbContext.Users.Add(user);
                if(addUser != null)
                { 
                    _employeDbContext.SaveChanges();
                    _logger.Info("User Added" + " " + user.Name);
                    return addUser.Entity;
                    
                }
                else
                {
                    _logger.Info("There was some Error, User Not Added");
                    return null;    
                }
            }
            catch (Exception ex) 
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;
               
            }
        }

        //To Get All Users
        public List<User> GetAllUsers() 
        {
            try
            {
                var gtuser = _employeDbContext.Users.ToList();
                if(gtuser != null)
                { 
                    return gtuser;
                }
                else
                {
                    return null;
                }
            }
            catch( Exception ex ) 
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;
            }
        
        }
        //To Update USer Details
        public User UpdateUser(User user)
        {
            try
            {
                PasswordManager passwordManager = new PasswordManager();
                string hashedPassword = passwordManager.HashPassword(user.Password);
                //Replacing the Plain Text Password With Encrypted Password
                user.Password = hashedPassword;
                var updateuser = _employeDbContext.Users.Update(user);
                if (updateuser !=null)
                {
                    
                    _employeDbContext.SaveChanges();
                    _logger.Info("User Details Updated");
                    return updateuser.Entity;

                }
                else
                {
                    _logger.Info("User Details not Updated");
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
        //To Delete a User
        public bool DeleteUser(int id)
        {
            try
            {
                var user = _employeDbContext.Users.SingleOrDefault(x => x.Id == id);
                if (user != null)
                {
                    _employeDbContext.Users.Remove(user);
                    _employeDbContext.SaveChanges();
                    _logger.Info( "User Deleted of Id"+" "+id );
                    return true;
                }
                else
                {
                    _logger.Info("There was Error ");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return false;
            }
        }


        //This Method is used to assign Role to the Given User
        public bool AssignRoleToUser(AddUserRole obj)
        {
            //This is to try and Assign Role to the User
            try
            { 
                var addRoles = new List<UserRole>(); 

                //To check whether we have Users existing in the  ModelClass Passed as a parameter in the method 
                var user = _employeDbContext.Users.SingleOrDefault(x => x.Id == obj.UserId);
                //If there are no Users in the Model Class
                if (user == null)
                {
                    _logger.Info("no users Found");
                }
                //User is valid and Exists

                foreach(int role in obj.RoleIds)
                {
                    //First Create a new instance of UserRoles  
                    //Add roleId and UserId  
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = user.Id;
                    //Adding the userRole instance into the addRoles List
                    addRoles.Add(userRole);

                }
                _employeDbContext.UserRoles.AddRange(addRoles);
                _employeDbContext.SaveChanges();
                _logger.Info("The Role / Roles has been assigned to the User");
                return true;
            }
            //If Role is Not added
            catch(Exception ex) 
            {
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return false;
            }

        }
        //In this Method we will First Validate the User Credentials 
        //Then if they are Valid we then create a JWT token for the Respective Login
        public string Login(LoginRequest loginRequest)
        {
            try
            { 
            //To check the username and password are not null
            if(loginRequest.Username !=null && loginRequest.Password !=null)
            {
               

                // To check whether the Entered Username and Password belongs to any user or not 
                var user = _employeDbContext.Users.SingleOrDefault(x=>x.Username==loginRequest.Username );

                //Converting and Comparing the Hashed Password with the plain text password
                PasswordManager passwordManager = new PasswordManager();
                bool IsMatchedPassword = passwordManager.VerifyPassword(loginRequest.Password, user.Password);

                if (user !=null && IsMatchedPassword==true)
                { 
                   
                        if (user.IsActive == true)
                        {
                            //Here we create a lIst of Claims which will store the information
                            var authclaims = new List<Claim>
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                                new Claim("Id",user.Id.ToString()),
                                new Claim(ClaimTypes.Name,user.Username),

                            };
                            // This is to  get all the Roles associated to the user(** a user can have Multiple Roles**)
                            var userRoles = _employeDbContext.UserRoles.Where(x => x.UserId == user.Id).ToList();
                            var roleIds = userRoles.Select(x => x.RoleId).ToList();
                            //The is to get the Role Name using the RoleId into a List
                            var roles = _employeDbContext.Roles.Where(x => roleIds.Contains(x.Id)).ToList();
                            foreach (var role in roles)
                            {
                                //We will store each role name from the list into Claims type Object 
                                authclaims.Add(new Claim(ClaimTypes.Role, role.Name,ClaimValueTypes.String));
                            }
                            //Method to Generate Token 
                            var genToken = GetToken(authclaims);
                            //This will write the Token 
                            var jwtToken = new JwtSecurityTokenHandler().WriteToken(genToken);
                            _logger.Info("JWT token has been Generated");
                            _logger.Info(loginRequest.Username + " " + "has Logged in ");
                            return jwtToken;
                        }
                        else
                        {
                        _logger.Info("User is InActive , Please Contact the Admin for Activation ");
                        
                            return "User is InActive , Please Contact the Admin for Activation ";
                        }
                    
                }
                //if User is not valid 
                else
                {
                    _logger.Info("Incorrect Username or Password");
                    return "Incorrect Username or Password";
                }
            }
            //If username or password is null
            else
            {_logger.Info("Username and Password can not be null");
                return "Username and Password can not be null";
            }
            
        }
        catch(Exception ex)
            {
               
                _logger.Info(ex.Message);
                _logger.Info(ex.InnerException);
                return null;

            }
        }

        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                   //we pass these parameters for creating the token from appsettings.json
                                   issuer: _configuration["Jwt:Issuer"],
                                   audience: _configuration["Jwt:Audience"],
                                   //Claims holds the information about the user
                                   claims: claims,
                                   //This is basically after how long the token will expire
                                   expires: DateTime.UtcNow.AddDays(10),
                                   signingCredentials: signin);
            return token;
        }

        public string GetRoleName()
        {
            return "Conflict is being Created to test";
        }
    }


}

