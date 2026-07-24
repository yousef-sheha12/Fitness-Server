using Fitness.Models;
using Fitness.Models.DTOs.Auth;

namespace Fitness.Interface.IService
{
    public interface IUserService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> UpdateAsync(int id, User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> VerifyOtpAsync(string email, string code);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string code, string newPassword);
    }
}
