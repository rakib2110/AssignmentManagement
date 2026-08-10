using AssignmentManagement.Dto;
using AssignmentManagement.Models;

namespace AssignmentManagement.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<RolesDto>> GetAllUserRoles();
        Task<User> CreateNewUser(UsersDto usersDto);
        Task<User?> UserLogin(LoginDto loginDto);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> EmailExists(string email);
        Task<bool> VerifyEmail(string token);
    }
}
