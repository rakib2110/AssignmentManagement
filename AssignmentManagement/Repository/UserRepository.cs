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
        private readonly IEmailService _emailService;
        public UserRepository(AssignmentManagementDbContext context, IPasswordHasher<User> passwordHasher, IEmailService emailService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<User> CreateNewUser(UsersDto usersDto)
        {
            var user = new User()
            {

                Firstname = usersDto.Firstname,
                Lastname = usersDto.Lastname,
                Email = usersDto.Email,
                Phone = usersDto.Phone,
                Roleid = usersDto.Roleid,
                Isemailverified = false,
                Isactive = true,
                Createdat = DateTime.Now,
                Updatedat= DateTime.Now

            };
            user.Passwordhash =_passwordHasher.HashPassword(user, usersDto.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var token =Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            // Create Verification Token

            var verificationToken =
                new EmailVerificationToken
                {

                    Userid = user.Userid,

                    Token = token,

                    Expiresat = DateTime.Now.AddMinutes(30),

                    Isused = false,

                    Createdat = DateTime.Now
                };

            // Save Verification Token

            await _context.EmailVerificationTokens.AddAsync(verificationToken);

            await _context.SaveChangesAsync();

            // Create Verification Link

            var verificationLink =$"https://localhost:7198/api/Auth/verify-email" +$"?token={Uri.EscapeDataString(token)}";

            // Send Verification Email

            await _emailService.SendVerificationEmail(
                user.Email,
                user.Firstname,
                verificationLink
            );

            return user;
        }
        public async Task<User?> UserLogin(LoginDto loginDto)
        {

            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(
                    x => x.Email == loginDto.Email
                );


            if (user == null)
            {
                return null;
            }

            if (user.Isemailverified != true)
            {
                return null;
            }

            if (user.Isactive != true)
            {
                return null;
            }

            var passwordResult =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.Passwordhash,
                    loginDto.Password
                );


            if (passwordResult ==
                PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }
        public async Task<IEnumerable<RolesDto>> GetAllUserRoles()
        {
            return await _context.Roles.AsNoTracking().Where(r => r.Roleid != 1)
                .Select(r => new RolesDto { 
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
        public async Task<bool> VerifyEmail(string token)
        {
            var verificationToken =
                await _context.EmailVerificationTokens
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x =>
                        x.Token == token &&
                        !x.Isused
                    );

            // Token not found or already used
            if (verificationToken == null)
            {
                return false;
            }

            if (verificationToken.Expiresat < DateTime.UtcNow)
            {
                return false;
            }

            var user = verificationToken.User;

            if (user == null)
            {
                return false;
            }

            user.Isemailverified = true;

            user.Isactive = true;

            user.Updatedat = DateTime.UtcNow;

            verificationToken.Isused = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
