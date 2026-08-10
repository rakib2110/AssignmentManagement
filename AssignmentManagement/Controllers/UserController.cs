using AssignmentManagement.Dto;
using AssignmentManagement.IRepository;
using AssignmentManagement.Models;
using AssignmentManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace AssignmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("UserRole")]
        public async Task<IActionResult> GetAllUserRoles()
        {
            try
            {
                var roles = await _userRepository.GetAllUserRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> Register([FromBody] UsersDto usersDto)
        {
            try
            {
                // Check whether email already exists
                var emailExists = await _userRepository.EmailExists(usersDto.Email);

                if (emailExists)
                {
                    throw new Exception("Email already exists.");
                }

                var user = await _userRepository.CreateNewUser(usersDto);

                return Ok(new
                {
                    message =
                        "Registration successful. " +
                        "Please check your email " +
                        "to verify your account."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("verifyemail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(new
                {
                    message =
                        "Verification token is required."
                });
            }

            var verified =
                await _userRepository.VerifyEmail(token);

            if (!verified)
            {
                return BadRequest(new
                {
                    message =
                        "Invalid or expired verification link."
                });
            }

            // For now return JSON.
            // Later you can redirect to React.
            return Ok(new
            {
                message =
                    "Email verified successfully. " +
                    "You can now login."
            });
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            // Login user

            var user =
                await _userRepository.UserLogin(loginDto);

            // Login failed

            if (user == null)
            {
                return Unauthorized(new
                {
                    message =
                        "Invalid email or password."
                });
            }

            // Generate JWT

            var token =
                GenerateJwtToken(user);

            // Login successful

            return Ok(new
            {
                message = "Login successful.",

                token = token,

                user = new
                {
                    userId = user.Userid,

                    firstName = user.Firstname,

                    lastName = user.Lastname,

                    email = user.Email,

                    phone = user.Phone,

                    roleId = user.Roleid,

                    roleName = user.Role?.Rolename
                }
            });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey =
                _configuration["Jwt:Key"];

            var jwtIssuer =
                _configuration["Jwt:Issuer"];

            var jwtAudience =
                _configuration["Jwt:Audience"];


            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "JWT Key is not configured."
                );
            }

            // Claims

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Userid.ToString()
                ),

                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Userid.ToString()
                ),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.Firstname
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role?.Rolename ?? ""
                ),

                new Claim(
                    "roleId",
                    user.Roleid.ToString()
                )
            };

            // Security Key

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                );

            // Credentials

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

            // Create JWT

            var jwtToken =
                new JwtSecurityToken(
                    issuer: jwtIssuer,

                    audience: jwtAudience,

                    claims: claims,

                    expires:
                        DateTime.UtcNow.AddHours(2),

                    signingCredentials:
                        credentials
                );

            // Convert JWT to string

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUsers()
        { 
            var users = await _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var userEmail = await _userRepository.GetUserByEmail(email);

            if (userEmail == null)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(userEmail);
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(user);
        }
    }
}
