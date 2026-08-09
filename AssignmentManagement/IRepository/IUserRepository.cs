using AssignmentManagement.Dto;
using AssignmentManagement.Models;

namespace AssignmentManagement.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<RolesDto>> GetAllUserRoles();
        Task<User> CreateNewUser(UsersDto usersDto);
        Task<User?> GetUserByEmail(string email);

        Task<User?> GetUserById(Guid userId);

        Task<IEnumerable<User>> GetAllUsers();

        Task<bool> EmailExists(string email);
    }
}
