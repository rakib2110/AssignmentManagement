using AssignmentManagement.Dto;
using AssignmentManagement.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace AssignmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        [Route("User Role")]
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
        [Route("Create User")]
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
                    message = "User created successfully.",
                    userId = user.Userid
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
        [Route("Get All User")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("Get User By Email")]
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
        [Route("Get User By Id")]
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
