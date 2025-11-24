using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure.Core;
using EmployeeManagementSystemAPI.Entities;
using EmployeeManagementSystemAPI.Interfaces;
using EmployeeManagementSystemAPI.UserDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystemAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        
        public async Task<User?> RegisterAsync(UserDTO request)
        {
          if(await _authRepository.GetUserByUserNameAsync(request.UserName) != null)
            {
                return null; // User already exists
            }
           User user = new User();
            // Hash the password
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;

            return await _authRepository.CreateUserAsync(user);
        }

        public async Task<string?> LoginAsync(UserDTO userDTOobj)
        {
            var user = await _authRepository.GetUserByUserNameAsync(userDTOobj.UserName);
           if(user == null)
            {
                return null; // User not found
            }

           var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDTOobj.Password);
             
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return CreateToken(user);
            
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }

    }
}
