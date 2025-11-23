
using EmployeeManagementSystemAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemAPI.Data
{
    public class UserDbContext : DbContext
    {
        // Constructor to accept DbContextOptions
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        // DbSet for Users table
        public DbSet<User> Users { get; set; }
      
    }
}
