using BookingSystem.Core.Interfaces;
using BookingSystem.Core.Models;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.Services
{
    public class UserService:IUserInterface
    {
        private readonly BookingSystemContext _context;
        private readonly IHasher _hasher;
        private readonly IConfiguration _configuration;

        public UserService(BookingSystemContext context, IConfiguration configuration, IHasher hasher)
        {
            _context = context;
            _configuration = configuration;
            _hasher = hasher;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<string> CreateUser(SignUpDto dto)
        {
            var existingUserName = DoesUserExist(dto.Username, true);
            var existingUserEmail = DoesUserExist(dto.Email, false);

            if (existingUserName)
                return "username";
            if (existingUserEmail)
                return "email";

            int role = await _context.Roles.Where(r => r.Title == "User").Select(u => u.Id).FirstOrDefaultAsync();
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                RoleId = role,
                JoinedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var loginCreds = new LoginCredentials
            {
                UserId = user.Id,
                UserName = user.UserName,
                HashedPassword = _hasher.CreateHash(dto.Password)
            };

            _context.LoginCredentials.Add(loginCreds);
            await _context.SaveChangesAsync();
            return $"{user.Id}";
        }

        public async Task<User?> Login(LoginDto dto)
        {
            var user = await _context.Users.Where(u => u.UserName == dto.Username).FirstOrDefaultAsync();

            if (user == null)
                return null;

            var loginCreds = await _context.LoginCredentials.Where(l => l.UserId == user.Id).FirstOrDefaultAsync();

            var hashedPassword = await _context.LoginCredentials.Where(l => l.UserId == user.Id).FirstOrDefaultAsync();
            if (hashedPassword == null)
                return null;

            if (!_hasher.ValidatePassword(dto.Password, hashedPassword.HashedPassword))
                return null;

            loginCreds.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return user;
        }

    }
}
