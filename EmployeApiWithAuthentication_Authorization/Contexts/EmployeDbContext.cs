using EmployeApiWithAuthentication_Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeApiWithAuthentication_Authorization.Contexts
{
    public class EmployeDbContext : DbContext
    {
        public EmployeDbContext(DbContextOptions<EmployeDbContext>options) : base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
