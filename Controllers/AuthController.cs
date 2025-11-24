using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeManagementSystemAPI.Entities;
using EmployeeManagementSystemAPI.Interfaces;
using EmployeeManagementSystemAPI.Services;
using EmployeeManagementSystemAPI.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await _authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("UserName already exists");
            }
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {                    
            var token = await _authService.LoginAsync(request);
            if(token == null)
            {
                return BadRequest("Invalid credentials");
            }
            return Ok(token);

        }
        
    }
}
