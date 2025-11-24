using EmployeeManagementSystemAPI.Entities;

namespace EmployeeManagementSystemAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<User> CreateUserAsync(User user);
    }
}
