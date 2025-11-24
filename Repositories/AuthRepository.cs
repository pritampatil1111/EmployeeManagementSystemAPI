using EmployeeManagementSystemAPI.Data;
using EmployeeManagementSystemAPI.Entities;
using EmployeeManagementSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserDbContext _context;

        public AuthRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?>  GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
               
    }
}
