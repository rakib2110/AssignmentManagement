using AssignmentManagement.Dto;
using AssignmentManagement.IRepository;
using AssignmentManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AssignmentManagementDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepository(AssignmentManagementDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher= passwordHasher;
        }

        public async Task<User> CreateNewUser(UsersDto usersDto)
        {
            var user = new User()
            {
                Userid = Guid.NewGuid(),

                Firstname = usersDto.Firstname,
                Lastname = usersDto.Lastname,
                Email = usersDto.Email,
                Phone = usersDto.Phone,

                Roleid = usersDto.Roleid,

                Isemailverified = false,
                Isactive = true,

                Createdat = DateTime.UtcNow

            };
            user.Passwordhash =_passwordHasher.HashPassword(user, usersDto.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<RolesDto>> GetAllUserRoles()
        {
            return await _context.Roles.AsNoTracking().Select(r => new RolesDto { 
                Id =r.Roleid,
                Name=r.Rolename
            }).ToListAsync();

            

        }
        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Userid == userId);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
