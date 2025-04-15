using BookingSystem.Core.Dtos;
using BookingSystem.Core.Models;

namespace BookingSystem.Core.Interfaces
{
    public interface IUserInterface
    {
        List<User> GetAllUsers();
        Task<string> CreateUser(SignUpDto dto);
        Task<User?> Login(LoginDto dto);
        bool DoesUserExist(string? value, bool name);
        Task<(string jwtToken, string refreshToken)> GenerateJwtToken(User user);
        string GenerateRefreshToken();
        Task StoreRefreshToken(User user, string refreshToken);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<User?> GetUserByToken(string refreshToken);
        Task<bool> SignOut(string userId);
        Task<bool> DeleteUser(string id);
    }
}
