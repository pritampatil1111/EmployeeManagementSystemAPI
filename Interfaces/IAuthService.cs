using EmployeeManagementSystemAPI.Entities;
using EmployeeManagementSystemAPI.UserDto;


namespace EmployeeManagementSystemAPI.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO userDto);
        Task<string?> LoginAsync(UserDTO userDto);
                
    }
}
